using EternalStore.Domain.NotificationManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EternalStore.DataAccess.NotificationManagement.Configuration
{
    public class SchedulerItemConfiguration : IEntityTypeConfiguration<SchedulerItem>
    {
        public void Configure(EntityTypeBuilder<SchedulerItem> builder)
        {
            builder.ToTable("schedulerItems_tb")
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
                .WithOne(sm => sm.SchedulerItem)
                .HasForeignKey<SchedulerMessage>("idSchedulerItem")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(s => s.Settings)
                .WithOne(ss => ss.SchedulerItem)
                .HasForeignKey<SchedulerSettings>("idSchedulerItem")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
