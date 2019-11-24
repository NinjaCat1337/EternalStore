using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EternalStore.DataAccess.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task Insert(T item);
        void Modify(object item);
        void Eliminate(object item);
        Task<T> Get(int id);
        Task<IEnumerable<T>> GetBy(Func<T, bool> predicate);
        Task SaveChangesAsync();
    }
}
