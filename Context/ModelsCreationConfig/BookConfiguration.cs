using Library.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Context.ModelsCreationConfig;

public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Title).IsRequired().HasMaxLength(255);
        builder.Property(b => b.ISBN).IsRequired();
        builder.Property(b => b.PublishedYear).IsRequired();
        
        builder.HasOne<Category>(c => c.Category).WithMany(b => b.Books).HasForeignKey(c => c.CategoryId).OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(a => a.Authors).WithMany(b => b.Books)
            .UsingEntity("AuthorBook");

    }
}