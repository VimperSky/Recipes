using Recipes.WebApi.DTO.Recipe;

namespace Recipes.WebApi.DTO
{
    public class RecipesPage
    {
        public RecipePreview[] Recipes { get; init; }
        public int PageCount { get; init; }
    }
}