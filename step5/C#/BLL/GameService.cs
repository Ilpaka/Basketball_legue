using BasketballLeague.DAL;
using BasketballLeague.Models;
using System;
using System.Collections.Generic;

namespace BasketballLeague.BLL
{
    public class GameService : BaseService<Game>
    {
        private readonly IGameRepository _repository;
        public GameService(IGameRepository repository) => _repository = repository;

        public override void Add(Game game)
        {
            if (game.Team1Id == game.Team2Id)
                throw new ArgumentException("Команды не могут быть одинаковыми.");
            _repository.Insert(game);
        }

        public override IEnumerable<Game> GetAll() => _repository.GetAll();
        public override Game GetById(int id) => _repository.GetById(id);
    }
}
