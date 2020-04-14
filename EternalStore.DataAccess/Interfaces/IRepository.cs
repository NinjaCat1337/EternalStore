using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EternalStore.DataAccess.Interfaces
{
    public interface IRepository<T> : IDisposable where T : class
    {
        IQueryable<T> GetAll();
        Task InsertAsync(T item);
        void Modify(object item);
        void Eliminate(object item);
        Task<T> GetAsync(int id);
        Task<IEnumerable<T>> GetByAsync(Func<T, bool> predicate);
        Task SaveChangesAsync();
    }
}
