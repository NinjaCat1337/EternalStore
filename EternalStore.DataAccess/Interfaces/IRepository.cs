using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EternalStore.DataAccess.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        void Insert(T product);
        void Modify(T product);
        void Eliminate(int id);
        T Get(int id);
        IEnumerable<T> GetBy(Func<T, bool> predicate);
        Task SaveChangesAsync();
    }
}
