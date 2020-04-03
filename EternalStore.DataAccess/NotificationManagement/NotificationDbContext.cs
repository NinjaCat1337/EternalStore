using EternalStore.DataAccess.NotificationManagement.Configuration;
using EternalStore.Domain.NotificationManagement;
using Microsoft.EntityFrameworkCore;

namespace EternalStore.DataAccess.NotificationManagement
{
    public sealed class NotificationDbContext : DbContext
    {
        private readonly string connectionString;

        public DbSet<Scheduler> Schedulers { get; set; }
        public DbSet<EmailMessage> EmailMessages { get; set; }

        public NotificationDbContext(string connectionString)
        {
            this.connectionString = connectionString;
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseSqlServer(connectionString);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new SchedulerConfiguration());
            modelBuilder.ApplyConfiguration(new SchedulerSettingsConfiguration());
            modelBuilder.ApplyConfiguration(new SchedulerMessageConfiguration());
            modelBuilder.ApplyConfiguration(new EmailMessageConfiguration());
        }
    }
}
