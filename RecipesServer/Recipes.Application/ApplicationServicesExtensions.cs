using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Recipes.Application.Services.Activity;
using Recipes.Application.Services.Recipes;
using Recipes.Application.Services.Tags;
using Recipes.Application.Services.User;

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
            services.AddScoped<JwtHandler>();
            services.AddScoped<IImageFileSaver, ImageFileSaver>();
            services.AddScoped<ITagsService, TagsService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRecipesService, RecipesService>();
            services.AddScoped<IActivityService, ActivityService>();
        }
    }
}