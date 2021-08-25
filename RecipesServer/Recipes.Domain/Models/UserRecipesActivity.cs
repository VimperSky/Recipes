using System.Collections.Generic;

namespace Recipes.Domain.Models
{
    public class UserRecipesActivity
    {
        public ICollection<int> LikedRecipes { get; set; }
        public ICollection<int> StarredRecipes { get; set; }
    }
}