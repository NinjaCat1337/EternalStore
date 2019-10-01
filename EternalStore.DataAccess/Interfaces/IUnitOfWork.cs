using EternalStore.Domain.Models;
using System;
using System.Threading.Tasks;

namespace EternalStore.DataAccess.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Product> Products { get; }

        Task SaveAsync();
    }
}
