using BasketballLeague.DAL;
using BasketballLeague.Models;
using System;
using System.Collections.Generic;

namespace BasketballLeague.BLL
{
    public class TeamService : BaseService<Team>
    {
        private readonly ITeamRepository _repository;
        public TeamService(ITeamRepository repository) => _repository = repository;

        public override void Add(Team team)
        {
            if (string.IsNullOrWhiteSpace(team.Name))
                throw new ArgumentException("Название команды не может быть пустым.");
            _repository.Insert(team);
        }

        public override IEnumerable<Team> GetAll() => _repository.GetAll();
        public override Team GetById(int id) => _repository.GetById(id);
    }
}
