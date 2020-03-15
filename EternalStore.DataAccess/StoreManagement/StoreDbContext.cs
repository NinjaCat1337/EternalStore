using EternalStore.DataAccess.StoreManagement.Configuration;
using EternalStore.Domain.StoreManagement;
using Microsoft.EntityFrameworkCore;

namespace EternalStore.DataAccess.StoreManagement
{
    public sealed class StoreDbContext : DbContext
    {
        private readonly string connectionString;

        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }

        public StoreDbContext(string connectionString)
        {
            this.connectionString = connectionString;
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseSqlServer(connectionString);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderItemConfiguration());
        }
    }
}
