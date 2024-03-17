using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReportEvcn.Domain.Entity;

namespace ReportEvcn.DAL.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Login).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Password).IsRequired();

            builder.HasMany(x => x.Reports)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .HasPrincipalKey(x => x.Id);

            ///РАЗБЕРИСЬ С ЭТИМ
            builder.HasMany(x => x.Roles)
                .WithMany(x => x.Users)
                .UsingEntity<UserRole>(l => l.HasOne<Role>().WithMany().HasForeignKey(x => x.RoleId),
                l => l.HasOne<User>().WithMany().HasForeignKey(x => x.UserId)
                );


            builder.HasData(new List<User>
            {
                new User()
                {
                    Id = 1,
                    Login = "Evcn",
                    Password = "ADSA@#DF444",
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = 1
                }
            });
        }
    }
}
