using EternalStore.Domain.OrderManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EternalStore.DataAccess.OrderManagement.Configuration
{
    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("orders_tb").HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasColumnName("idOrder")
                .HasColumnType("int")
                .IsRequired();

            builder.Property(p => p.IsApproved)
                .HasColumnName("isApproved")
                .HasColumnType("bit")
                .IsRequired();

            builder.Property(p => p.OrderDate)
                .HasColumnName("orderDate")
                .HasColumnType("datetime2")
                .IsRequired();

            builder.Property(p => p.DeliveryDate)
                .HasColumnName("deliveryDate")
                .HasColumnType("datetime2")
                .IsRequired();

            builder.Property(p => p.CustomerName)
                .HasColumnName("customerName")
                .HasColumnType("nvarchar(50)")
                .IsRequired();

            builder.Property(p => p.CustomerNumber)
                .HasColumnName("customerNumber")
                .HasColumnType("nvarchar(30)")
                .IsRequired();

            builder.Property(p => p.CustomerAddress)
                .HasColumnName("customerAddress")
                .HasColumnType("nvarchar(100)")
                .IsRequired();

            builder.Property(p => p.AdditionalInformation)
                .HasColumnName("additionalInformation")
                .HasColumnType("nvarchar(100)")
                .IsRequired(false);

            builder.Property(p => p.IsDelivered)
                .HasColumnName("isDelivered")
                .HasColumnType("bit")
                .IsRequired();

            builder.Metadata.FindNavigation(nameof(Order.OrderItems))
                .SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.HasMany(o => o.OrderItems)
                .WithOne(oi => oi.Order)
                .HasForeignKey("idOrder")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
