using EternalStore.DataAccess.EntityFramework;
using EternalStore.DataAccess.Interfaces;
using EternalStore.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace EternalStore.DataAccess.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EternalStoreDbContext dbContext;
        private ProductRepository productRepository;
        private bool disposed;

        public UnitOfWork(DbContextOptions options) => dbContext = new EternalStoreDbContext(options);

        public IRepository<Product> Products => productRepository ?? (productRepository = new ProductRepository(dbContext));

        public void SaveAsync() => dbContext.SaveChangesAsync();

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
