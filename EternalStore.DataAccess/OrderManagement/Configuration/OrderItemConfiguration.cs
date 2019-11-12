using EternalStore.Domain.OrderManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EternalStore.DataAccess.OrderManagement.Configuration
{
    internal class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("orderItems_tb").HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasColumnName("idOrderItem")
                .HasColumnType("int")
                .IsRequired();

            builder.Property(p => p.Name)
                .HasColumnName("name")
                .HasColumnType("nvarchar(50)")
                .IsRequired();

            builder.Property(p => p.Qty)
                .HasColumnName("qty")
                .HasColumnType("int")
                .IsRequired();
        }
    }
}
