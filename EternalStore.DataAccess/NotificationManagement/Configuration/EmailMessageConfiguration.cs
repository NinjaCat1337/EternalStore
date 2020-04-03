using System;
using EternalStore.Domain.NotificationManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EternalStore.DataAccess.NotificationManagement.Configuration
{
    public class EmailMessageConfiguration : IEntityTypeConfiguration<EmailMessage>
    {
        public void Configure(EntityTypeBuilder<EmailMessage> builder)
        {
            builder.ToTable("emailMessages_tb")
                .HasKey(p => p.Id);

            builder.Property(p => p.SendingDate)
                .HasColumnName("sendingDate")
                .HasColumnType("datetime2")
                .IsRequired();

            builder.Property(p => p.RecipientEmail)
                .HasColumnName("recipientEmail")
                .HasColumnType("nvarchar(150)")
                .IsRequired();

            builder.Property(p => p.SenderEmail)
                .HasColumnName("senderEmail")
                .HasColumnType("nvarchar(150)")
                .IsRequired();

            builder.HasOne(p => p.Message)
                .WithMany(sm => sm.EmailMessages)
                .HasForeignKey("idSchedulerMessage");
        }
    }
}
