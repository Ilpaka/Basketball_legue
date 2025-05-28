using BasketballLeague.DAL;
using BasketballLeague.Models;
using System;
using System.Collections.Generic;

namespace BasketballLeague.BLL
{
    public class CoachService : BaseService<Coach>
    {
        private readonly ICoachRepository _repository;
        public CoachService(ICoachRepository repository) => _repository = repository;

        public override void Add(Coach coach)
        {
            if (coach.ExperienceYears < 0)
                throw new ArgumentException("Опыт не может быть отрицательным.");
            _repository.Insert(coach);
        }

        public override IEnumerable<Coach> GetAll() => _repository.GetAll();
        public override Coach GetById(int id) => _repository.GetById(id);
    }
}
