using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Recipes.Application.Services.Auth;
using Recipes.Application.Services.Auth.HelpClasses;
using Recipes.Application.Services.Auth.Models;
using Recipes.Application.Services.Recipes;

namespace Recipes.Application
{
    public static class ApplicationServicesExtensions
    {
        public static void AddAuthorization(this IServiceCollection services, IConfigurationSection jwtSection)
        {
            services.Configure<JwtSettings>(jwtSection);
            var jwtSettings = new JwtSettings();
            jwtSection.Bind(jwtSettings);

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.ValidIssuer,
                    ValidAudience = jwtSettings.ValidAudience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecurityKey))
                };
            });
        }
        
        public static void AddApplicationDependencies(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ApplicationServicesExtensions));

            services.AddScoped<AuthService>();  
            services.AddScoped<RecipesService>();
            services.AddScoped<JwtHandler>();
        }
    }
}