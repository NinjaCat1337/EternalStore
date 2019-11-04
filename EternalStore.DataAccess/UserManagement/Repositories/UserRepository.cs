using EternalStore.DataAccess.Interfaces;
using EternalStore.Domain.UserManagement;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EternalStore.DataAccess.UserManagement.Repositories
{
    public class UserRepository : IRepository<User>, IDisposable
    {
        private readonly UsersDbContext dbContext;
        private bool disposed;

        public UserRepository(string connectionString) => dbContext = new UsersDbContext(connectionString);

        public IEnumerable<User> GetAll() => dbContext.Users;

        public IEnumerable<User> GetBy(Func<User, bool> predicate) => dbContext.Users.Where(predicate).ToList();

        public void Insert(User user) => dbContext.Users.Add(user);

        public void Modify(User user) => dbContext.Entry(user).State = EntityState.Modified;

        public User Get(int id) => dbContext.Users.Find(id);

        public void Eliminate(int id)
        {
            var user = dbContext.Users.Find(id);

            if (user != null) dbContext.Users.Remove(user);
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

        ~UserRepository() => Dispose(false);
    }
}
