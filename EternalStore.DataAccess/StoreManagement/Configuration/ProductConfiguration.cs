using EternalStore.Domain.StoreManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EternalStore.DataAccess.StoreManagement.Configuration
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("products_tb").HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasColumnName("idProduct")
                .HasColumnType("int")
                .IsRequired();

            builder.Property(p => p.Name)
                .HasColumnName("name")
                .HasColumnType("nvarchar(50)")
                .IsRequired();

            builder.Property(p => p.Description)
                .HasColumnName("description")
                .HasColumnType("nvarchar(1500)")
                .IsRequired();

            builder.Property(p => p.Price)
                .HasColumnName("price")
                .HasColumnType("decimal")
                .IsRequired();

            builder.HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey("idCategory");
        }
    }
}
