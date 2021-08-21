using Recipes.Application.DTOs.User;

namespace Recipes.Application.DTOs.Recipe
{
    public class RecipePreviewDto : RecipeBaseDto
    {
        public int Id { get; init; }
        public string ImagePath { get; init; }
        public AuthorDto Author { get; init; }
    }
}