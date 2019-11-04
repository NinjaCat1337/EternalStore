using EternalStore.Domain.UserManagement;
using Microsoft.EntityFrameworkCore;

namespace EternalStore.DataAccess.UserManagement
{
    public sealed class UsersDbContext : DbContext
    {
        private string ConnectionString { get; set; }

        public DbSet<User> Users { get; set; }

        public UsersDbContext(string connectionString)
        {
            ConnectionString = connectionString;
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseSqlServer(ConnectionString);
    }
}
