using System;
using battleSimulator.Models;

namespace battleSimulator.ViewModels
{
    public class ArmyViewModel
    {
        public string Name { get; set; }
        public int Units { get; set; }
        public AttackStrategy AttackStrategy { get; set; }

        internal Army Get()
        {
            return new Army() {
                Name = Name,
                Units = Units,
                AliveUnits = Units,
                AttackStrategy = AttackStrategy
            };
        }
    }
}