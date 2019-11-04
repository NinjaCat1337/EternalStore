using EternalStore.DataAccess.Interfaces;
using EternalStore.Domain.StoreManagement;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EternalStore.DataAccess.StoreManagement.Repositories
{
    public class StoreRepository : IRepository<Category>, IDisposable
    {
        private readonly StoreDbContext dbContext;
        private bool disposed;

        public StoreRepository(string connectionString) => dbContext = new StoreDbContext(connectionString);

        public IEnumerable<Category> GetAll() => dbContext.Categories;

        public IEnumerable<Category> GetBy(Func<Category, bool> predicate) => dbContext.Categories.Where(predicate).ToList();

        public void Insert(Category category) => dbContext.Categories.Add(category);

        public void Modify(Category category) => dbContext.Entry(category).State = EntityState.Modified;

        public Category Get(int id) => dbContext.Categories.Find(id);

        public void Eliminate(int id)
        {
            var category = dbContext.Categories.Find(id);

            if (category != null) dbContext.Categories.Remove(category);
        }

        public async Task SaveChangesAsync() => await dbContext.SaveChangesAsync();

        private void Dispose(bool disposing)
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

        ~StoreRepository() => Dispose(false);
    }
}
