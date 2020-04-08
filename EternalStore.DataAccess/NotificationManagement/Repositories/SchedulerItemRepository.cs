using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EternalStore.DataAccess.Interfaces;
using EternalStore.Domain.NotificationManagement;
using Microsoft.EntityFrameworkCore;

namespace EternalStore.DataAccess.NotificationManagement.Repositories
{
    public class SchedulerItemRepository : IRepository<SchedulerItem>
    {
        private readonly NotificationDbContext dbContext;
        private bool disposed;

        public SchedulerItemRepository(string connectionString) => dbContext = new NotificationDbContext(connectionString);

        public IQueryable<SchedulerItem> GetAll() => dbContext.Schedulers
            .Include(si => si.Message)
            .Include(si => si.Settings)
            .AsQueryable();

        public async Task InsertAsync(SchedulerItem item) => await dbContext.AddAsync(item);

        public void Modify(object item)
        {
            if (item as SchedulerItem != null || item as SchedulerMessage != null || item as SchedulerSettings != null)
                dbContext.Entry(item).State = EntityState.Modified;

            else
                throw new Exception("Wrong type.");
        }

        public void Eliminate(object item)
        {
            if (item as SchedulerItem != null || item as SchedulerMessage != null || item as SchedulerSettings != null)
                dbContext.Entry(item).State = EntityState.Deleted;

            else
                throw new Exception("Wrong type.");
        }

        public async Task<SchedulerItem> GetAsync(int id)
        {
            var schedulerItem = await dbContext.Schedulers
                .Include(s => s.Message)
                .Include(s => s.Settings)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (schedulerItem == null)
                throw new Exception("Scheduler not found.");

            return schedulerItem;
        }

        public async Task<IEnumerable<SchedulerItem>> GetByAsync(Func<SchedulerItem, bool> predicate)
        {
            var schedulerItems = await dbContext.Schedulers
                .Include(s => s.Settings)
                .Include(s => s.Message)
                .ToListAsync();

            return schedulerItems.Where(predicate);
        }

        public async Task SaveChangesAsync() => await dbContext.SaveChangesAsync();

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

        ~SchedulerItemRepository() => Dispose(false);
    }
}
