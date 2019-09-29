using EternalStore.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EternalStore.DataAccess.EntityFramework
{
    public sealed class EternalStoreDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public EternalStoreDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
