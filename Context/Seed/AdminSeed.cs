using Library.Model.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Context.Seed;

public class AdminSeed : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        string adminUserId = "29E23309-9F18-4652-8279-E86EA6B634CC";

        var adminUser = new User
        {
            Id = adminUserId,
            Document = "1234567",
            Name = "ADMIN",
            UserName = "ADMIN",
            NormalizedUserName = "ADMIN",
            Email = "admin@library.com",
            NormalizedEmail = "ADMIN@LIBRARY.COM",
            SecurityStamp = Guid.NewGuid().ToString(),
            AccessFailedCount = 0,
            EmailConfirmed = true,
            LockoutEnabled = false,
            PhoneNumberConfirmed = false
        };

        var passwordHasher = new PasswordHasher<User>();
        adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, "Admin@123456");

        builder.HasData(adminUser);
    }
}