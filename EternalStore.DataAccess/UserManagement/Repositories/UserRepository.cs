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

        /// <summary>
        /// Get all Users from database.
        /// </summary>
        /// <returns>IEnumerable collection of Users.</returns>
        public async Task<IEnumerable<User>> GetAllAsync() => await dbContext.Users.ToListAsync();

        /// <summary>
        /// Get User by predicate from database.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>User entity.</returns>
        public async Task<IEnumerable<User>> GetByAsync(Func<User, bool> predicate)
        {
            var result = await dbContext.Users.ToListAsync();
            return result.Where(predicate);
        }

        /// <summary>
        /// Add User to database.
        /// </summary>
        /// <param name="user">User entity.</param>
        public async Task InsertAsync(User user) => await dbContext.Users.AddAsync(user);

        /// <summary>
        /// Modify User/UserInformation/UserAddress/UserNumber in database.
        /// </summary>
        /// <param name="item">Should be User/UserInformation type.</param>
        public void Modify(object item)
        {
            if (item as User != null || item as UserInformation != null)
                dbContext.Entry(item).State = EntityState.Modified;

            else
                throw new Exception("Wrong type.");
        }

        /// <summary>
        /// Get User by Id from database.
        /// </summary>
        /// <param name="id">Id user.</param>
        /// <returns>User entity.</returns>
        public async Task<User> GetAsync(int id)
        {
            var user = await dbContext.Users
                .Include(u => u.UserInformation)
                .Include(u => u.UserAddresses)
                .Include(u => u.UserNumbers)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (user == null)
                throw new Exception("User not found.");

            return user;
        }

        /// <summary>
        /// Delete User and UserInformation or UserAddress or UserNumber from database.
        /// </summary>
        /// <param name="item">Should be an User/UserAddress/UserNumber type.</param>
        public void Eliminate(object item)
        {
            if (item as User != null || item as UserAddress != null || item as UserNumber != null)
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

        ~UserRepository() => Dispose(false);
    }
}
