using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Context.Seed;

public class RolesSeed : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        IdentityRole internalUser = new IdentityRole
        {
            Id = "1A7E2F64-9A1E-4A9E-9B57-2F3028E3A02D", // GUID fixo
            Name = "InternalUser",
            NormalizedName = "INTERNALUSER" 
        };

        IdentityRole customer = new IdentityRole
        {
            Id = "2B9E3F75-ABCD-4D5E-8C56-3D1238E4B03E", // GUID fixo
            Name = "customer",
            NormalizedName = "CUSTOMER"
        };

        builder.HasData(internalUser, customer);
    }
}