using Library.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Context.ModelsCreationConfig;

public class AuthorConfiguration : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> authorBuilder)
    {
       authorBuilder.HasKey(a => a.Id);
       authorBuilder.Property(a => a.Name).IsRequired().HasMaxLength(255);
    }
}