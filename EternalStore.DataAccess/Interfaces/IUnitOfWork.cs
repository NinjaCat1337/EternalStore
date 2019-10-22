using EternalStore.Domain.OrderManagement;
using EternalStore.Domain.ProductManagement;
using System;
using System.Threading.Tasks;

namespace EternalStore.DataAccess.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Product> Products { get; }
        IRepository<Category> Categories { get; }
        IRepository<Order> Orders { get; }

        Task SaveAsync();
    }
}
