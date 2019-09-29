using EternalStore.Domain.Models;
using System;

namespace EternalStore.DataAccess.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Product> Products { get; }

        void SaveAsync();
    }
}
