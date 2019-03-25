using System.Linq;

namespace OutReachDataAccessLayer.Generic_Repository
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> SelectAll();
        T SelectByID(object id);
        void Insert(T obj);
        void Update(T obj);
        void Delete(T obj);
        void Save();

    }
}
