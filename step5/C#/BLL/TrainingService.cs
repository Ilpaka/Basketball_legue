using BasketballLeague.DAL;
using BasketballLeague.Models;
using System;
using System.Collections.Generic;

namespace BasketballLeague.BLL
{
    public class TrainingService : BaseService<Training>
    {
        private readonly ITrainingRepository _repository;
        public TrainingService(ITrainingRepository repository) => _repository = repository;

        public override void Add(Training training)
        {
            if (string.IsNullOrWhiteSpace(training.Place))
                throw new ArgumentException("Место тренировки не может быть пустым.");
            _repository.Insert(training);
        }

        public override IEnumerable<Training> GetAll() => _repository.GetAll();
        public override Training GetById(int id) => _repository.GetById(id);
    }
}
