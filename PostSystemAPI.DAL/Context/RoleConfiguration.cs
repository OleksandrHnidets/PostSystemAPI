using Common.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PostSystemAPI.DAL.Context
{
    public class RoleConfiguration: IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                {
                    Name = Roles.Viewer.ToString(),
                    NormalizedName = "VIEWER"
                },
                new IdentityRole
                {
                    Name = Roles.Administrator.ToString(),
                    NormalizedName = "ADMINISTRATOR"
                },
                new IdentityRole
                {
                    Name = Roles.Driver.ToString(),
                    NormalizedName = "DRIVER"
                }
           );
        }
    }
}
