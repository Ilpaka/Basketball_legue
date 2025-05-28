using System;

namespace BasketballLeague.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public int Number { get; set; }
        public float Height { get; set; }
        public float Weight { get; set; }
        public string Position { get; set; }
        public int TeamId { get; set; }
        public int Points { get; set; }
        public int Assists { get; set; }
        public int Rebounds { get; set; }
    }
}
