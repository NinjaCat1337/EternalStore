using EternalStore.Domain.NotificationManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EternalStore.DataAccess.NotificationManagement.Configuration
{
    public class SchedulerConfiguration : IEntityTypeConfiguration<Scheduler>
    {
        public void Configure(EntityTypeBuilder<Scheduler> builder)
        {
            builder.ToTable("schedulers_tb")
                .HasKey(s => s.Id);

            builder.Property(p => p.Id)
                .HasColumnName("idScheduler")
                .HasColumnType("int")
                .IsRequired();

            builder.Property(p => p.Name)
                .HasColumnName("name")
                .HasColumnType("nvarchar(100)")
                .IsRequired();

            builder.Property(p => p.ExecutionDateTime)
                .HasColumnName("executionDateTime")
                .HasColumnType("datetime2");

            builder.HasOne(s => s.Message)
                .WithOne(sm => sm.Scheduler)
                .HasForeignKey("idScheduler")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(s => s.Settings)
                .WithOne(ss => ss.Scheduler)
                .HasForeignKey("idScheduler")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
