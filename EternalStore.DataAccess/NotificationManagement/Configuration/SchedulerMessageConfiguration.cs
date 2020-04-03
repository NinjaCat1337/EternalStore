using EternalStore.Domain.NotificationManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EternalStore.DataAccess.NotificationManagement.Configuration
{
    public class SchedulerMessageConfiguration : IEntityTypeConfiguration<SchedulerMessage>
    {
        public void Configure(EntityTypeBuilder<SchedulerMessage> builder)
        {
            builder.ToTable("schedulerMessages_tb")
                .HasKey(p => p.Id);

            builder.Property(p => p.Subject)
                .HasColumnName("subject")
                .HasColumnType("nvarchar(150)")
                .IsRequired();

            builder.Property(p => p.Body)
                .HasColumnName("body")
                .HasColumnType("nvarchar(max)")
                .IsRequired();
        }
    }
}
