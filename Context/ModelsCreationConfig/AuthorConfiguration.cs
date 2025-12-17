using Library.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Context.ModelsCreationConfig;

public class AuthorConfiguration : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
       builder.HasKey(a => a.Id);
       builder.Property(a => a.Name).IsRequired().HasMaxLength(255);
       builder.Property(a => a.CreatedAt).IsRequired();
    }
}