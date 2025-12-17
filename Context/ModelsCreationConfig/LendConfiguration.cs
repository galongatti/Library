using Library.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Context.ModelsCreationConfig;

public class LendConfiguration : IEntityTypeConfiguration<Lend>
{
    public void Configure(EntityTypeBuilder<Lend> builder)
    {
        builder.HasKey(l => l.Id);
        builder.HasOne<User>(l => l.InternalUser).WithMany(b => b.LendsAsInternalUser).HasForeignKey(c => c.InternalUserId).OnDelete(DeleteBehavior.Restrict);  
        
        builder.HasOne<User>(l => l.Costumer).WithMany(b => b.LendsAsCostumer).HasForeignKey(c => c.CostumerUserId).OnDelete(DeleteBehavior.Restrict);

        builder.Property(l => l.ExpectedReturnDate);
        builder.Property(l => l.Status).IsRequired();
        builder.Property(l => l.CreatedAt).IsRequired();
        builder.Property(l => l.LendDate).IsRequired();
    }
}