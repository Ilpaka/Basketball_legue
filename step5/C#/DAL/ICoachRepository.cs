using System.Collections.Generic;
using BasketballLeague.Models;

namespace BasketballLeague.DAL
{
    public interface ICoachRepository
    {
        void Insert(Coach coach);
        IEnumerable<Coach> GetAll();
        Coach GetById(int id);
    }
}
