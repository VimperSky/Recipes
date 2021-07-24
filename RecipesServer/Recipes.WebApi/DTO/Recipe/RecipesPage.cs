namespace Recipes.WebApi.DTO.Recipe
{
    public class RecipesPage
    {
        public RecipePreview[] Recipes { get; init; }
        public int PageCount { get; init; }
    }
}