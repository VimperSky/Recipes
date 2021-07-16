using System;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Recipes.Domain;
using Recipes.Infrastructure;

namespace Recipes.WebApi.Tests.HelpClasses
{
    public class TestWebFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup: class   
    {
        private IConfiguration Configuration { get; set; }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration(config =>
            {
                Configuration = new ConfigurationBuilder()
                    .AddJsonFile("settings.json")
                    .Build();
                
                config.AddConfiguration(Configuration);
            });

            builder.ConfigureServices(services =>
            {
                services.Remove(services.SingleOrDefault(d => d.ServiceType == typeof(IDomainConfig)));
                services.AddScoped<IDomainConfig, TestDomainConfig>();
                
                using var scope = services.BuildServiceProvider().CreateScope();
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<RecipesContext>();

                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
                
                TestDbCreator.FillWithStartData(db);
            });
        }
    }
}