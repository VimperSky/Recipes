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
    public static class Config
    {
        public static void ConfigureInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IRecipesRepository, RecipesRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            
            // services.AddIdentity<User, IdentityRole>(opt =>
            //     {
            //         opt.User.RequireUniqueEmail = true
            //     })
            //     .AddEntityFrameworkStores<RecipesDbContext>();
        }

        public static void ConfigureDatabase(this IServiceCollection services, string connectionString)
        {
            services.AddDatabaseDeveloperPageExceptionFilter();
            
            services.AddDbContext<RecipesDbContext>(options => 
                options.UseNpgsql(connectionString, 
                        x => x.MigrationsAssembly("Recipes.WebApi"))
                    .UseSnakeCaseNamingConvention());
        }
    }
}