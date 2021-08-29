using System.Collections.Generic;

namespace Recipes.Domain.Models
{
    public class Tag
    {
        public string Value { get; set; }

        public string Icon { get; set; }

        public TagLevel TagLevel { get; set; } = TagLevel.Regular;

        public string Description { get; set; }

        public ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();

        public static Tag Create(string value,
            TagLevel tagLevel = TagLevel.Regular, string iconPath = null, string description = null)
        {
            return new Tag
            {
                Value = value,
                TagLevel = tagLevel,
                Icon = iconPath,
                Description = description
            };
        }
    }
}