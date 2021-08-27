namespace Recipes.Application.Models.Recipe
{
    public class RecipesPageResult
    {
        public RecipePreviewResult[] Recipes { get; init; }
        public int PageCount { get; init; }
    }
}