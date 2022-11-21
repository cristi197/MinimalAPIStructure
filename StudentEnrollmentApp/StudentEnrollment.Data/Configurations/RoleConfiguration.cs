using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StudentEnrollment.Data.Configurations
{
    internal class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                {
                    Id = "3f96c1ea-6bd6-4c2e-b7b5-7f64e9b7ac42",
                    Name = "Administrator",
                    NormalizedName="ADMINISTRATOR"
                },
                new IdentityRole
                {
                    Id = "15d2bddc-2d9d-4fab-9ad2-acea8533e0b8",
                    Name = "User",
                    NormalizedName = "USER"
                }
            );
        }
    }
}
