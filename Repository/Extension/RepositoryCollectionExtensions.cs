using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository.Data;
using Repository.Repositories;

public static class RepositoryCollectionExtensions
{
    public static void AddRepositoryLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            //options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            options.UseSqlite(configuration.GetConnectionString("sqlite"));
            options.EnableSensitiveDataLogging();
        }
        );




    }
}
