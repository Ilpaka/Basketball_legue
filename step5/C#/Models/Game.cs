using System;

namespace BasketballLeague.Models
{
    public class Game
    {
        public int Id { get; set; }
        public int Team1Id { get; set; }
        public int Team2Id { get; set; }
        public DateTime Date { get; set; }
        public int Score1 { get; set; }
        public int Score2 { get; set; }
    }
}
