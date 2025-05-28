using System.Collections.Generic;
using BasketballLeague.Models;

namespace BasketballLeague.DAL
{
    public interface IPlayerRepository
    {
        void Insert(Player player);
        IEnumerable<Player> GetAll();
        Player GetById(int id);
        Player GetByName(string name, string surname);
    }
}
