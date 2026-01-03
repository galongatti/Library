using Library.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Context.ModelsCreationConfig;

public class LendItemConfiguration : IEntityTypeConfiguration<LendItem>
{
    public void Configure(EntityTypeBuilder<LendItem> builder)
    {
        builder.HasKey(li => li.Id);

        builder.HasOne(li => li.Lend)
               .WithMany(l => l.Items)
               .HasForeignKey(li => li.LendId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(li => li.BookCopy)
               .WithMany()
               .HasForeignKey(li => li.BookCopyId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}

