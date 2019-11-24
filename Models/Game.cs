using System.Collections.Generic;

namespace battleSimulator.Models
{
    public class Game
    {
        public string Id { get; set; }
        public List<Army> Armies { get; set; }
        public Status Status { get; set; }

        public Game()
        {
            Armies = new List<Army>();
            Status = Status.Open;
        }
    }

    public enum Status
    {
        Open,
        InProgress,
        Closed
    }
}