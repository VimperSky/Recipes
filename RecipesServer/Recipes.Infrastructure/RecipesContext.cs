using Microsoft.EntityFrameworkCore;
using Recipes.Domain.Models;

namespace Recipes.Infrastructure
{
    public class RecipesContext: DbContext
    {
        public DbSet<Recipe> Recipes { get; init; }

        public RecipesContext(DbContextOptions<RecipesContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Recipe>().ToTable("recipe");
            modelBuilder.Entity<Recipe>().OwnsMany(x => x.IngredientBlocks, 
                y => y.ToTable("recipe_ingredient_block"));
        }
    }
}