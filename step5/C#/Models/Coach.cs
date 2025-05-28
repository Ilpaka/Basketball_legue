using System;

namespace BasketballLeague.Models
{
    public class Coach
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public int ExperienceYears { get; set; }
        public string Specialization { get; set; }
        public int TeamId { get; set; }
    }
}