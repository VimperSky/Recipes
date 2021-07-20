using Recipes.Domain.Models;

namespace Recipes.WebApi.DTO.Recipe
{
    public class Ingredient
    {
        public string Header { get; init; }
        public string Value { get; init; }
        
        public static Ingredient FromModel(RecipeIngredientBlock ingredientBlock)
        {
            return new()
            {
                Header = ingredientBlock.Header, Value = ingredientBlock.Text
            };
        }
    }
}