using battleSimulator.ViewModels;

namespace battleSimulator.Services
{
    public interface IGameService
    {
        bool AddArmy(ArmyViewModel armyVM);
        bool Start(out int armiesCount);
    }
}