using EternalStore.DataAccess.UserManagement.Configurations;
using EternalStore.Domain.UserManagement;
using Microsoft.EntityFrameworkCore;

namespace EternalStore.DataAccess.UserManagement
{
    public sealed class UsersDbContext : DbContext
    {
        private readonly string connectionString;

        public DbSet<User> Users { get; set; }

        public UsersDbContext(string connectionString)
        {
            this.connectionString = connectionString;
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseSqlServer(connectionString);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserInformationConfiguration());
            modelBuilder.ApplyConfiguration(new UserAddressConfiguration());
            modelBuilder.ApplyConfiguration(new UserNumberConfiguration());
        }
    }
}
