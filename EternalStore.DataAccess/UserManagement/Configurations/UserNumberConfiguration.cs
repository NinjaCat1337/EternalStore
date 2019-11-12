using EternalStore.Domain.UserManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EternalStore.DataAccess.UserManagement.Configurations
{
    internal class UserNumberConfiguration : IEntityTypeConfiguration<UserNumber>
    {
        public void Configure(EntityTypeBuilder<UserNumber> builder)
        {
            builder.ToTable("userNumbers_tb").HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasColumnName("idUserNumber")
                .HasColumnType("int")
                .IsRequired();

            builder.Property(p => p.Number)
                .HasColumnName("number")
                .HasColumnType("nvarchar(30)")
                .IsRequired();
        }
    }
}
