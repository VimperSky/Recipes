using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Recipes.Infrastructure;

namespace Recipes.Migrations
{
    public class RecipesDbContextFactory : IDesignTimeDbContextFactory<RecipesDbContext>
    {
        public RecipesDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true)
                .AddEnvironmentVariables()
                .Build();

            var builder = new DbContextOptionsBuilder<RecipesDbContext>();

            var connectionString = configuration.GetConnectionString("MigrationsConnection");
            
            builder.ConfigureDatabase(connectionString);

            return new RecipesDbContext(builder.Options);
        }
    }
}