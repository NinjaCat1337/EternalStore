using EternalStore.DataAccess.EntityFramework;
using EternalStore.DataAccess.Interfaces;
using EternalStore.Domain.OrderManagement;
using EternalStore.Domain.ProductManagement;
using System;
using System.Threading.Tasks;

namespace EternalStore.DataAccess.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EternalStoreDbContext dbContext;
        private ProductRepository productRepository;
        private CategoryRepository categoryRepository;
        private OrderRepository orderRepository;
        private bool disposed;

        public UnitOfWork(string connectionString) => dbContext = new EternalStoreDbContext(connectionString);

        public IRepository<Product> Products => productRepository ?? (productRepository = new ProductRepository(dbContext));
        public IRepository<Category> Categories => categoryRepository ?? (categoryRepository = new CategoryRepository(dbContext));
        public IRepository<Order> Orders => orderRepository ?? (orderRepository = new OrderRepository(dbContext));

        public async Task SaveAsync() => await dbContext.SaveChangesAsync();

        public virtual void Dispose(bool disposing)
        {
            if (disposed) return;
            if (disposing) dbContext.Dispose();

            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
