using System.Collections.Generic;
using BasketballLeague.Models;

namespace BasketballLeague.DAL
{
    public interface IGameRepository
    {
        void Insert(Game game);
        IEnumerable<Game> GetAll();
        Game GetById(int id);
    }
}
