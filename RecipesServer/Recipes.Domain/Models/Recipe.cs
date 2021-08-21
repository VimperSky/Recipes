using System;
using System.Collections.Generic;

namespace Recipes.Domain.Models
{
    public class Recipe
    {
        public int Id { get; set; }

        public User Author { get; set; }

        public int? AuthorId { get; set; }

        public string ImagePath { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public ushort CookingTimeMin { get; set; }
        public ushort Portions { get; set; }

        public string[] Steps { get; set; } = Array.Empty<string>();

        public ICollection<RecipeIngredientsBlock> Ingredients { get; set; } = new List<RecipeIngredientsBlock>();
        
        public ICollection<Tag> Tags { get; set; } 
    }
}