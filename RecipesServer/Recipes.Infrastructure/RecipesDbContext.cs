using Microsoft.EntityFrameworkCore;
using Recipes.Domain.Models;
using Recipes.Infrastructure.Configurations;

namespace Recipes.Infrastructure
{
    public class RecipesDbContext: DbContext
    {
        public DbSet<Recipe> Recipes { get; init; }
        public DbSet<User> Users { get; init; }

        public RecipesDbContext(DbContextOptions<RecipesDbContext> options) : base(options)
        {
        }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RecipeConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }
}