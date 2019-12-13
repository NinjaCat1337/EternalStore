using EternalStore.Domain.UserManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EternalStore.DataAccess.UserManagement.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users_tb").HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasColumnName("idUser")
                .HasColumnType("int")
                .IsRequired();

            builder.Property(p => p.Login)
                .HasColumnName("login")
                .HasColumnType("varchar(50)")
                .IsRequired();

            builder.Property(p => p.Password)
                .HasColumnName("password")
                .HasColumnType("nvarchar(100)")
                .IsRequired();

            builder.Property(p => p.RegistrationDate)
                .HasColumnName("registrationDate")
                .HasColumnType("datetime2")
                .IsRequired();

            builder.Property(p => p.Role)
                .HasColumnName("role")
                .HasColumnType("int")
                .IsRequired();

            builder.Metadata.FindNavigation(nameof(User.UserAddresses))
                .SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.Metadata.FindNavigation(nameof(User.UserNumbers))
                .SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.HasOne(u => u.UserInformation)
                .WithOne(ui => ui.User)
                .HasForeignKey<UserInformation>(ui => ui.Id)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.UserAddresses)
                .WithOne(ua => ua.User)
                .HasForeignKey("idUser")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.UserNumbers)
                .WithOne(un => un.User)
                .HasForeignKey("idUser")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
