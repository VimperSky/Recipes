namespace Recipes.WebApi.DTO.Recipe
{
    public class RecipesPageDto
    {
        public RecipePreviewDto[] Recipes { get; init; }
        public int PageCount { get; init; }
    }
}