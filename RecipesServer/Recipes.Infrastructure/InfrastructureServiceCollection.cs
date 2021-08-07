using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Recipes.Domain;
using Recipes.Domain.Models;
using Recipes.Domain.Repositories;
using Recipes.Infrastructure.Repositories;

namespace Recipes.Infrastructure
{
    public static class InfrastructureServiceCollection
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IRecipesRepository, RecipesRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            
            services.AddDatabaseDeveloperPageExceptionFilter();
        }

        public static void ConfigureDatabase(this IServiceCollection serviceCollection, string connectionString)
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