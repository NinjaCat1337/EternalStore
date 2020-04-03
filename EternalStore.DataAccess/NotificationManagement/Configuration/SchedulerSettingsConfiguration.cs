using System;
using EternalStore.Domain.NotificationManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EternalStore.DataAccess.NotificationManagement.Configuration
{
    public class SchedulerSettingsConfiguration : IEntityTypeConfiguration<SchedulerSettings>
    {
        public void Configure(EntityTypeBuilder<SchedulerSettings> builder)
        {
            builder.ToTable("schedulerSettings_tb")
                .HasKey(p => p.Id);

            builder.Property(p => p.ExecutionFrequency)
                .HasColumnName("executionFrequency")
                .HasColumnType("int")
                .IsRequired();

            builder.Property(p => p.ExecutionHours)
                .HasColumnName("executionHours")
                .HasColumnType("int")
                .IsRequired();

            builder.Property(p => p.ExecutionMinutes)
                .HasColumnName("executionMinutes")
                .HasColumnType("int")
                .IsRequired();

            builder.Property(p => p.ExecutionDayOfWeek)
                .HasColumnName("executionDayOfWeek")
                .HasColumnType("int");
        }
    }
}
