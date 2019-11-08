using EternalStore.Domain.UserManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EternalStore.DataAccess.UserManagement.Configurations
{
    internal class UserAddressConfiguration : IEntityTypeConfiguration<UserAddress>
    {
        public void Configure(EntityTypeBuilder<UserAddress> builder)
        {
            builder.ToTable("userAddresses_tb").HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasColumnName("idUserAddress")
                .HasColumnType("int")
                .IsRequired();

            builder.Property(p => p.Address)
                .HasColumnName("address")
                .HasColumnType("nvarchar")
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
