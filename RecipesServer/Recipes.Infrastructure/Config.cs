﻿using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Recipes.Domain;
using Recipes.Domain.Repositories;
using Recipes.Infrastructure.Repositories;

namespace Recipes.Infrastructure
{
    public static class Config
    {
        public static void ConfigureInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IRecipesRepository, RecipesRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public static void ConfigureDatabase(this IServiceCollection services, string connectionString)
        {
            services.AddDatabaseDeveloperPageExceptionFilter();
            
            services.AddDbContext<RecipesContext>(options => 
                options.UseNpgsql(connectionString, 
                        x => x.MigrationsAssembly("Recipes.WebApi"))
                    .UseSnakeCaseNamingConvention());
        }
    }
}