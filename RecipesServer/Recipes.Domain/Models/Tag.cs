using System.Collections.Generic;

namespace Recipes.Domain.Models
{
    public class Tag
    {
        public string Value { get; set; }

        public bool IsSelected { get; set; } = false;

        public string Description { get; set; } = null;

        public ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();

        public static Tag Create(string value, bool isSelected = false)
        {
           return new Tag
           {
               Value = value,
               IsSelected = isSelected
           };
        }
    }
}