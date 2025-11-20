using Library.Repository;
using Library.Services;

namespace Library.Configurations;

public static class RegisterServices
{

    public static void ConfigureServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthorRepository, AuthorRepository>();
        services.AddScoped<IAuthorService, AuthorService>();
    }
    
}