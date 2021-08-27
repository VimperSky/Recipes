namespace Recipes.Domain.Models
{
    public class Activity
    {
        public int RecipeId { get; init; }
        public int UserId { get; init; }
        public bool IsLiked { get; set; }
        public bool IsStarred { get; set; }
    }
}