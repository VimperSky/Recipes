using Microsoft.Extensions.DependencyInjection;
using Recipes.Application.Services.Auth;
using Recipes.Application.Services.Recipes;

namespace Recipes.Application
{
    public static class ApplicationServiceCollection
    {
        public static void Configure(this IServiceCollection services)
        {
            services.AddScoped<AuthService>();  
            services.AddScoped<RecipesService>();
        }
    }
}