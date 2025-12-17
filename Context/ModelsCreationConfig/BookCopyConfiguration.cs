using Library.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Context.ModelsCreationConfig;

public class BookCopyConfiguration : IEntityTypeConfiguration<BookCopy>
{
    public void Configure(EntityTypeBuilder<BookCopy> builder)
    {
        builder.HasKey(bc => bc.Id);
        builder.Property(bc => bc.Barcode).IsRequired().HasMaxLength(100);
        builder.Property(bc => bc.IsAvailable).IsRequired();

        builder.HasOne(bc => bc.Book)
               .WithMany(b => b.Copies)
               .HasForeignKey(bc => bc.BookId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}

