using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using battleSimulator.Data;
using battleSimulator.Models;
using battleSimulator.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace battleSimulator.Services
{
    public class GameService : IGameService
    {
        private AppDbContext _dbContext;

        public GameService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool AddArmy(ArmyViewModel armyVM)
        {
            if(_dbContext.Games.Any(x=>x.Status == Status.InProgress)) return false;

            Army army = armyVM.Get();
            Game game = GetOpenGame() ?? new Game();
            game.Armies.Add(army);

            return _dbContext.SaveChanges() > 0;
        }

        public bool Start(out int armiesCount)
        {
            armiesCount = 0;
            Game game = GetOpenGame();
            if(game == null) return false;
            armiesCount = game.Armies.Count;
            if(armiesCount < 10)
            {
                return false;
            }
            else
            {
                game.Status = Status.InProgress;
                StartBattle(game);
                return true;
            }
            
        }

        private async Task StartBattle(Game game)
        {
            Random random = new Random();
            foreach(Army army in game.Armies)
            {
                bool attackWillSuccess = random.Next(0, army.Units) <= army.AliveUnits;
                List<Army> otherArmies = game.Armies.ToList();
                otherArmies.Remove(army);
                if(attackWillSuccess)
                {
                    if(army.AttackStrategy == AttackStrategy.Random)
                    {
                        otherArmies[random.Next(0, otherArmies.Count)].AliveUnits -= 0.5f;
                    }
                    else if(army.AttackStrategy == AttackStrategy.Weakest)
                    {
                        float minAU = otherArmies.Min(x=> x.AliveUnits);
                        otherArmies.First(x=> x.AliveUnits == minAU).AliveUnits -= 0.5f;
                    }
                    else if(army.AttackStrategy == AttackStrategy.Strongest)
                    {
                        float maxAU = otherArmies.Max(x=> x.AliveUnits);
                        otherArmies.First(x=> x.AliveUnits == maxAU).AliveUnits -= 0.5f;
                    }
                }
            }

            //await Task.Sleep(0.01);
            if(game.Armies.Count(x=> x.AliveUnits > 0) > 1)
                StartBattle(game);
            else
                game.Status = Status.Closed;

            _dbContext.SaveChanges();
        }

        private Game GetOpenGame()
        {
            return _dbContext.Games.Include(x => x.Armies).LastOrDefault(x => x.Status == Status.Open);
        }
    }
}