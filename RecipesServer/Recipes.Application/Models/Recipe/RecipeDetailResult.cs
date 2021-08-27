namespace Recipes.Application.Models.Recipe
{
    public class RecipeDetailResult: RecipePreviewResult
    {
        public Ingredient[] Ingredients { get; init; }

        public string[] Steps { get; init; }
    }
}