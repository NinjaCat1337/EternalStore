using EternalStore.Domain.StoreManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EternalStore.DataAccess.StoreManagement.Configuration
{
    internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("categories_tb").HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasColumnName("idCategory")
                .HasColumnType("int")
                .IsRequired();

            builder.Property(p => p.Name)
                .HasColumnName("name")
                .HasColumnType("nvarchar")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(p => p.IsEnabled)
                .HasColumnName("isEnabled")
                .HasColumnType("bit")
                .IsRequired();

            builder.Metadata.FindNavigation(nameof(Category.Products))
                .SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.HasMany(c => c.Products)
                .WithOne(p => p.Category)
                .HasForeignKey("idCategory")
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
