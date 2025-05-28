using System.Collections.Generic;
using BasketballLeague.Models;

namespace BasketballLeague.DAL
{
    public interface ITeamRepository
    {
        void Insert(Team team);
        IEnumerable<Team> GetAll();
        Team GetById(int id);
    }
}
