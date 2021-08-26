namespace Recipes.WebApi.DTOs.Recipe
{
    public class RecipesPageResultDTO
    {
        public RecipePreviewResultDTO[] Recipes { get; init; }
        public int PageCount { get; init; }
    }
}