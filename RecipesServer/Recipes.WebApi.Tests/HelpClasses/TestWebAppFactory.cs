using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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