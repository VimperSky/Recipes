using Recipes.WebApi.DTOs.User;

namespace Recipes.WebApi.DTOs.Recipe
{
    public class RecipePreviewResultDTO : RecipeBaseDto
    {
        public int Id { get; init; }
        public string ImagePath { get; init; }
        public AuthorDto Author { get; init; }

        public int LikesCount { get; set; }
        public int StarsCount { get; set; }

        public bool IsLiked { get; set; }

        public bool IsStarred { get; set; }
    }
}