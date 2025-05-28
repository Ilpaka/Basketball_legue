using System.Collections.Generic;
using BasketballLeague.Models;

namespace BasketballLeague.DAL
{
    public interface ITrainingRepository
    {
        void Insert(Training training);
        IEnumerable<Training> GetAll();
        Training GetById(int id);
    }
}
