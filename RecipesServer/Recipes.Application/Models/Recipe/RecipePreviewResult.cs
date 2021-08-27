using Recipes.Application.Models.User;

namespace Recipes.Application.Models.Recipe
{
    public class RecipePreviewResult: RecipeBase
    {
        public int Id { get; init; }
        public string ImagePath { get; init; }
        public AuthorInfo Author { get; init; }
        
        public int LikesCount { get; set; }
        public int StarsCount { get; set; }
        
        public bool IsLiked { get; set; }
        
        public bool IsStarred { get; set; }
    }
}