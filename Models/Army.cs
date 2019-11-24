namespace battleSimulator.Models
{
    public class Army
    {
        public string Id { get; set; }
        public string Name {get; set; }
        public int Units {get; set; }
        public float AliveUnits { get; set; }
        public AttackStrategy AttackStrategy {get; set;}
    }

    public enum AttackStrategy
    {
        Random,
        Weakest,
        Strongest
    }
}