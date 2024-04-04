
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReportEvcn.Domain.Entity;
using ReportEvcn.Domain.Enum;

namespace ReportEvcn.DAL.Configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);


            builder.HasData(new List<Role>
            {
                new Role()
                {
                    Id = Guid.NewGuid(),
                    Name = nameof(Roles.User)
                },
                new Role()
                {
                    Id = Guid.NewGuid(),
                    Name = nameof(Roles.Admin)
                },
                new Role()
                {
                    Id = Guid.NewGuid(),
                    Name = nameof(Roles.Moderator)
                },

            });
        }
    }
}
