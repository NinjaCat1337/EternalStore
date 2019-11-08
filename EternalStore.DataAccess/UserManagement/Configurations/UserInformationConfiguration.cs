using EternalStore.Domain.UserManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EternalStore.DataAccess.UserManagement.Configurations
{
    internal class UserInformationConfiguration : IEntityTypeConfiguration<UserInformation>
    {
        public void Configure(EntityTypeBuilder<UserInformation> builder)
        {

            builder.ToTable("usersInformation_tb").HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasColumnName("idUserInformation")
                .HasColumnType("int")
                .IsRequired();

            builder.Property(p => p.FirstName)
                .HasColumnName("firstName")
                .HasColumnType("nvarchar")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(p => p.LastName)
                .HasColumnName("lastName")
                .HasColumnType("nvarchar")
                .HasMaxLength(50)
                .IsRequired(false);

            builder.Property(p => p.Email)
                .HasColumnName("email")
                .HasColumnType("varchar")
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
