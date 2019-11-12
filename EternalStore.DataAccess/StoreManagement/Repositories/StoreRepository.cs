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

        /// <summary>
        /// Get all categories from database.
        /// </summary>
        /// <returns>IEnumerable collection of Categories.</returns>
        public IEnumerable<Category> GetAll() => dbContext.Categories;

        /// <summary>
        /// Get Categories by predicate from database.
        /// </summary>
        /// <param name="predicate">Predicate.</param>
        /// <returns></returns>
        public IEnumerable<Category> GetBy(Func<Category, bool> predicate) => dbContext.Categories.Where(predicate).ToList();

        /// <summary>
        /// Add Category to database.
        /// </summary>
        /// <param name="category">Category entity.</param>
        public void Insert(Category category) => dbContext.Categories.Add(category);

        /// <summary>
        /// Update Product or Category in database.
        /// </summary>
        /// <param name="item">Should be an Product or Category type.</param>
        public void Modify(object item)
        {
            if (item as Product != null || item as Category != null)
                dbContext.Entry(item).State = EntityState.Modified;
        }

        /// <summary>
        /// Get Category from database by Id.
        /// </summary>
        /// <param name="id">Id Category.</param>
        /// <returns></returns>
        public Category Get(int id)
        {
            var category = dbContext.Categories.Include(c => c.Products).FirstOrDefault(c => c.Id == id);

            if (category == null)
                throw new Exception("Category not found.");

            return category;
        }

        /// <summary>
        /// Delete Category or Product from database.
        /// </summary>
        /// <param name="item">Should by an Product or Category type.</param>
        public void Eliminate(object item)
        {
            if (item as Product != null || item as Category != null)
                dbContext.Entry(item).State = EntityState.Deleted;
        }

        /// <summary>
        /// Save changes in database.
        /// </summary>
        public async Task SaveChangesAsync() => await dbContext.SaveChangesAsync();

        /// <summary>
        /// Close connection with database.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposed) return;
            if (disposing) dbContext.Dispose();

            disposed = true;
        }



        ~StoreRepository() => Dispose(false);
    }
}
