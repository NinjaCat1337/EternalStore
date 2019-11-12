using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EternalStore.DataAccess.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        void Insert(T item);
        void Modify(object item);
        void Eliminate(object item);
        T Get(int id);
        IEnumerable<T> GetBy(Func<T, bool> predicate);
        Task SaveChangesAsync();
    }
}
