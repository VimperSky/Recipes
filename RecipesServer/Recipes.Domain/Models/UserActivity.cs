using System.Collections.Generic;

namespace Recipes.Domain.Models
{
    public class UserActivity
    {
        public ICollection<int> LikedRecipes { get; set; }
        public ICollection<int> StarredRecipes { get; set; }
    }
}