using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EternalStore.DataAccess.Interfaces;
using EternalStore.Domain.NotificationManagement;
using Microsoft.EntityFrameworkCore;

namespace EternalStore.DataAccess.NotificationManagement.Repositories
{
    public class EmailMessageRepository : IRepository<EmailMessage>
    {
        private readonly NotificationDbContext dbContext;
        private bool disposed;

        public EmailMessageRepository(string connectionString) => dbContext = new NotificationDbContext(connectionString);

        public IQueryable<EmailMessage> GetAll() => dbContext.EmailMessages.AsQueryable();

        public async Task InsertAsync(EmailMessage item) => await dbContext.AddAsync(item);

        public void Modify(object item)
        {
            if (item as EmailMessage != null)
                dbContext.Entry(item).State = EntityState.Modified;

            else
                throw new Exception("Wrong type.");
        }

        public void Eliminate(object item)
        {
            if (item as EmailMessage != null)
                dbContext.Entry(item).State = EntityState.Deleted;

            else
                throw new Exception("Wrong type.");
        }

        public async Task<EmailMessage> GetAsync(int id)
        {
            var email = await dbContext.EmailMessages.Include(em => em.Message).ThenInclude(sm => sm.Scheduler)
                .FirstOrDefaultAsync(em => em.Id == id);

            if (email == null)
                throw new Exception("Email not found.");

            return email;
        }

        public async Task<IEnumerable<EmailMessage>> GetByAsync(Func<EmailMessage, bool> predicate)
        {
            var allEmails = await dbContext.EmailMessages.ToListAsync();

            return allEmails.Where(predicate);
        }

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

        ~EmailMessageRepository() => Dispose(false);
    }
}
