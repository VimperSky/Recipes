using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Recipes.Domain;
using Recipes.Domain.Repositories;
using Recipes.Infrastructure.Repositories;

namespace Recipes.Infrastructure
{
    public static class InfrastructureServicesExtensions
    {
        public static void AddInfrastructureDependencies(this IServiceCollection services)
        {
            services.AddScoped<IRecipesRepository, RecipesRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            
            services.AddDatabaseDeveloperPageExceptionFilter();
        }

        public static void AddDatabase(this IServiceCollection serviceCollection, string connectionString)
        {
            serviceCollection.AddDbContext<RecipesDbContext>(options =>
                ConfigureDatabase(options, connectionString));
        }
        
        public static void ConfigureDatabase(this DbContextOptionsBuilder dbOptions, string connectionString)
        {
            dbOptions.UseNpgsql(connectionString, b =>
                b.MigrationsAssembly("Recipes.Migrations")).UseSnakeCaseNamingConvention();
        }
    }
}