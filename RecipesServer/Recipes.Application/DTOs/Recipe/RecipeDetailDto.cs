namespace Recipes.Application.DTOs.Recipe
{
    public class RecipeDetailDto : RecipePreviewDto
    {
        public IngredientDto[] Ingredients { get; init; }

        public string[] Steps { get; init; }
    }
}