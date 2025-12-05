using Library.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Context.ModelsCreationConfig;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> categoryBuilder)
    {
       categoryBuilder.HasKey(a => a.Id);
       categoryBuilder.Property(a => a.Name).IsRequired().HasMaxLength(255);
    }
}