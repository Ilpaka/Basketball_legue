using BasketballLeague.DAL;
using BasketballLeague.Models;
using System;
using System.Collections.Generic;

namespace BasketballLeague.BLL
{
    public class PlayerService : BaseService<Player>
    {
        private readonly IPlayerRepository _repository;
        public PlayerService(IPlayerRepository repository) => _repository = repository;

        public override void Add(Player player)
        {
            if (player.Height < 150 || player.Height > 250)
                throw new ArgumentException("Рост игрока должен быть в диапазоне 150-250 см.");
            _repository.Insert(player);
        }

        public override IEnumerable<Player> GetAll() => _repository.GetAll();
        public override Player GetById(int id) => _repository.GetById(id);

        public Player GetByName(string name, string surname) => _repository.GetByName(name, surname);
    }
}
