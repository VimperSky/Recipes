namespace Recipes.WebApi.DTOs.Recipe
{
    public class RecipeDetailResultResultDTO : RecipePreviewResultDTO
    {
        public IngredientDto[] Ingredients { get; init; }

        public string[] Steps { get; init; }
    }
}