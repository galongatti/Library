using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Context.Seed;

public class UserRoleSeed : IEntityTypeConfiguration<IdentityUserRole<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
    {
        builder.HasData(new IdentityUserRole<string>
        {
            UserId = "29E23309-9F18-4652-8279-E86EA6B634CC",
            RoleId = "1A7E2F64-9A1E-4A9E-9B57-2F3028E3A02D"
        });
    }
}