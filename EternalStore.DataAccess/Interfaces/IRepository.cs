using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EternalStore.DataAccess.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(int? skip, int? take, bool? ascending);
        Task InsertAsync(T item);
        void Modify(object item);
        void Eliminate(object item);
        Task<T> GetAsync(int id);
        Task<IEnumerable<T>> GetByAsync(Func<T, bool> predicate);
        Task SaveChangesAsync();
    }
}
