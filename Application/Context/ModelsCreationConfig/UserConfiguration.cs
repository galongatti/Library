using Library.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Context.ModelsCreationConfig;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(a => a.Name).IsRequired().HasMaxLength(255);
        builder.Property(a => a.Document).IsRequired().HasMaxLength(255);
    }
}