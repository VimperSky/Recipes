﻿using System.Collections.Generic;

namespace Recipes.Domain.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        
        public string ImagePath { get; set; }

        public string Name { get; init; }
        public string Description { get; init; }

        public ushort CookingTimeMin { get; init; }
        public ushort Portions { get; init; }

        public string[] Steps { get; init; } = System.Array.Empty<string>();

        public ICollection<RecipeIngredientBlock> IngredientBlocks { get; init; } = new List<RecipeIngredientBlock>();
    }
}