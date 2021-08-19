using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Recipes.Domain.Models;

namespace Recipes.Infrastructure.Configurations
{
    public class RecipeConfiguration : IEntityTypeConfiguration<Recipe>
    {
        public void Configure(EntityTypeBuilder<Recipe> builder)
        {
            builder.ToTable("recipe");
            builder.OwnsMany(x => x.Ingredients,
                y => y.ToTable("recipe_ingredient_block"));

            builder.Property(x => x.Name).IsRequired();
        }
    }
}