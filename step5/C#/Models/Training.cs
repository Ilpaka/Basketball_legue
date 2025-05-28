using System;

namespace BasketballLeague.Models
{
    public class Training
    {
        public int Id { get; set; }
        public int TeamId { get; set; }
        public DateTime Date { get; set; }
        public string Place { get; set; }
        public string Description { get; set; }
    }
}
