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
        /// Get the specified amount of categories, ordered by Id.
        /// </summary>
        /// <param name="skip">Values to skip.</param>
        /// <param name="take">Values to take.</param>
        /// <param name="ascending">Default: true</param>
        /// <returns>IEnumerable collection of Categories.</returns>
        public async Task<IEnumerable<Category>> GetAllAsync(int? skip = null, int? take = null, bool? ascending = null)
        {
            var query = dbContext.Set<Category>().AsQueryable();

            if (ascending != null)
                query = (bool)ascending ? query.OrderBy(o => o.Id) : query.OrderByDescending(o => o.Id);

            if (skip != null)
                query = query.Skip(skip.Value);

            if (take != null)
                query = query.Take(take.Value);

            return await query.ToListAsync();
        }

        /// <summary>
        /// Get Categories by predicate from database.
        /// </summary>
        /// <param name="predicate">Predicate.</param>
        /// <returns></returns>
        public async Task<IEnumerable<Category>> GetByAsync(Func<Category, bool> predicate) =>
            await dbContext.Categories.Where(predicate).AsQueryable().ToListAsync();

        /// <summary>
        /// Add Category to database.
        /// </summary>
        /// <param name="category">Category entity.</param>
        public async Task InsertAsync(Category category) => await dbContext.Categories.AddAsync(category);

        /// <summary>
        /// Update Product or Category in database.
        /// </summary>
        /// <param name="item">Should be an Product or Category type.</param>
        public void Modify(object item)
        {
            if (item as Product != null || item as Category != null)
                dbContext.Entry(item).State = EntityState.Modified;

            else
                throw new Exception("Wrong type.");
        }

        /// <summary>
        /// Get Category from database by Id.
        /// </summary>
        /// <param name="id">Id Category.</param>
        /// <returns></returns>
        public async Task<Category> GetAsync(int id)
        {
            var category = await dbContext.Categories.Include(c => c.Products).FirstOrDefaultAsync(c => c.Id == id);

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

            else
                throw new Exception("Wrong type.");
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
