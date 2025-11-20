using Library.Context.ModelsCreationConfig;
using Library.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Context;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Author> Authors { get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new AuthorConfiguration());
    }
}