using System.Collections.Generic;
using battleSimulator.Services;
using battleSimulator.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace battleSimulator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post(ArmyViewModel armyVM)
        {
            if(_gameService.AddArmy(armyVM))
                return Ok("Army added succesfully!");
            else
                return BadRequest("Army not added! There is already game in progress!");
        }

        [HttpGet("Start")]
        public IActionResult Start()
        {
            if(_gameService.Start(out int armiesCount))
                return Ok($"Game started succesfully between {armiesCount} armies!");
            else
                return BadRequest($"You are not able to start a game, because there are only {armiesCount} armies.");
        }
    }
}