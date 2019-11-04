using EternalStore.Domain.StoreManagement;
using Microsoft.EntityFrameworkCore;

namespace EternalStore.DataAccess.StoreManagement
{
    public sealed class StoreDbContext : DbContext
    {
        private string ConnectionString { get; set; }

        public DbSet<Category> Categories { get; set; }

        public StoreDbContext(string connectionString)
        {
            ConnectionString = connectionString;
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseSqlServer(ConnectionString);
    }
}
