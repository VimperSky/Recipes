namespace Recipes.Application.DTOs.Recipe
{
    public class RecipesPageDto
    {
        public RecipePreviewDto[] Recipes { get; init; }
        public int PageCount { get; init; }
    }
}