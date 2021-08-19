using Microsoft.EntityFrameworkCore;
using Recipes.Domain.Models;
using Recipes.Infrastructure.Configurations;

namespace Recipes.Infrastructure
{
    public class RecipesDbContext : DbContext
    {
        public RecipesDbContext(DbContextOptions<RecipesDbContext> options) : base(options)
        {
        }

        public DbSet<Recipe> Recipes { get; init; }
        public DbSet<User> Users { get; init; }
        
        public DbSet<Tag> Tags { get; init; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TagConfiguration());
            modelBuilder.ApplyConfiguration(new RecipeConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }
}