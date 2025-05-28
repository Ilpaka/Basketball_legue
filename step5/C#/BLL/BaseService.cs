using System.Collections.Generic;

namespace BasketballLeague.BLL
{
    public abstract class BaseService<T>
    {
        public abstract void Add(T entity);
        public abstract IEnumerable<T> GetAll();
        public abstract T GetById(int id);
    }
}
