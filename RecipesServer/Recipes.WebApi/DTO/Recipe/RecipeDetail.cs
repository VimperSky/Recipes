using System.Linq;

namespace Recipes.WebApi.DTO.Recipe
{
    public class RecipeDetail: RecipeBase
    {
        public int Id { get; init; }
        public Ingredient[] Ingredients { get; init; }
        public string[] Steps { get; init; }
        
        public static RecipeDetail FromModel(Domain.Models.Recipe recipe)
        {
            return new()
            {
                Id = recipe.Id, Description = recipe.Description, Name = recipe.Name, Portions = recipe.Portions,
                CookingTime = recipe.CookingTimeMin, ImagePath = recipe.ImagePath, 
                Ingredients = recipe.IngredientBlocks.Select(Ingredient.FromModel).ToArray(), Steps = recipe.Steps
            };
        }
    }
}