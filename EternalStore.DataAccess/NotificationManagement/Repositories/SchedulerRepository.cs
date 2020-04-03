using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EternalStore.DataAccess.Interfaces;
using EternalStore.Domain.NotificationManagement;
using Microsoft.EntityFrameworkCore;

namespace EternalStore.DataAccess.NotificationManagement.Repositories
{
    public class SchedulerRepository : IRepository<Scheduler>
    {
        private readonly NotificationDbContext dbContext;
        private bool disposed;

        public SchedulerRepository(string connectionString) => dbContext = new NotificationDbContext(connectionString);

        public IQueryable<Scheduler> GetAll() => dbContext.Schedulers.AsQueryable();

        public async Task InsertAsync(Scheduler item) => await dbContext.AddAsync(item);

        public void Modify(object item)
        {
            if (item as Scheduler != null || item as SchedulerMessage != null || item as SchedulerSettings != null)
                dbContext.Entry(item).State = EntityState.Modified;

            else
                throw new Exception("Wrong type.");
        }

        public void Eliminate(object item)
        {
            if (item as Scheduler != null || item as SchedulerMessage != null || item as SchedulerSettings != null)
                dbContext.Entry(item).State = EntityState.Deleted;

            else
                throw new Exception("Wrong type.");
        }

        public async Task<Scheduler> GetAsync(int id)
        {
            var scheduler = await dbContext.Schedulers
                .Include(s => s.Message)
                .Include(s => s.Settings)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (scheduler == null)
                throw new Exception("Scheduler not found.");

            return scheduler;
        }

        public async Task<IEnumerable<Scheduler>> GetByAsync(Func<Scheduler, bool> predicate)
        {
            var schedulers = await dbContext.Schedulers
                .Include(s => s.Settings)
                .Include(s => s.Message)
                .ToListAsync();

            return schedulers.Where(predicate);
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

        ~SchedulerRepository() => Dispose(false);
    }
}
