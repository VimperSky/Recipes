namespace Recipes.WebApi.DTOs.Recipe
{
    public class RecipeDetailResultDTO : RecipePreviewResultDTO
    {
        public IngredientDto[] Ingredients { get; init; }

        public string[] Steps { get; init; }
    }
}