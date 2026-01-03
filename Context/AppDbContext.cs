using Library.Context.ModelsCreationConfig;
using Library.Context.Seed;
using Library.Model.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Library.Context;

public class AppDbContext : IdentityDbContext<User>
{
    
    private static readonly ILoggerFactory _loggerFactory =
        LoggerFactory.Create(builder =>
        {
            builder
                .AddConsole()
                .AddFilter((category, level) =>
                    category == DbLoggerCategory.Database.Command.Name &&
                    level == LogLevel.Information);
        });
    
    public DbSet<Author> Authors { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<BookCopy> BookCopies { get; set; }
    public DbSet<Lend> Lends { get; set; }
    public DbSet<LendItem> LendItems { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder
                .UseLoggerFactory(_loggerFactory)
                .EnableSensitiveDataLogging() // mostra valores dos parâmetros
                .EnableDetailedErrors();      // mensagens mais detalhadas
        }
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        
        base.OnModelCreating(builder);
        
        builder.ApplyConfiguration(new RolesSeed());
        
        builder.ApplyConfiguration(new CategoryConfiguration());
        builder.ApplyConfiguration(new BookConfiguration());
        builder.ApplyConfiguration(new BookCopyConfiguration());
        builder.ApplyConfiguration(new LendItemConfiguration());
        builder.ApplyConfiguration(new AuthorConfiguration());
        builder.ApplyConfiguration(new UserConfiguration());
        builder.ApplyConfiguration(new LendConfiguration());
        
    }
}