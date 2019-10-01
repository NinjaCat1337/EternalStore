using EternalStore.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EternalStore.DataAccess.EntityFramework
{
    public sealed class EternalStoreDbContext : DbContext
    {
        private string ConnectionString { get; set; }
        public DbSet<Product> Products { get; set; }

        public EternalStoreDbContext(string connectionString)
        {
            ConnectionString = connectionString;
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseSqlServer(ConnectionString);
    }
}
