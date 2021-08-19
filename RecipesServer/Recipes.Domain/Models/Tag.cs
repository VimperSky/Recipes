using System.Collections.Generic;

namespace Recipes.Domain.Models
{
    public class Tag
    {
        public string Value { get; set; }

        public TagLevel TagLevel { get; set; } = TagLevel.Regular;

        public string Description { get; set; } = null;

        public ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();

        public static Tag Create(string value, TagLevel tagLevel = TagLevel.Regular)
        {
           return new Tag
           {
               Value = value,
               TagLevel = tagLevel
           };
        }
    }
}