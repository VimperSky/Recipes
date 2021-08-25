﻿using System.Linq;
using Recipes.Domain.Models;

namespace Recipes.Infrastructure
{
    public static class DatabaseExtensions
    {
        public static IQueryable<Recipe> SortByType(this IQueryable<Recipe> recipes, RecipesType recipesType,
            int authorId)
        {
            return recipesType switch
            {
                RecipesType.Own when authorId != default => recipes.Where(x => x.AuthorId == authorId),
                
                RecipesType.Starred when authorId != default => 
                    recipes.Where(r => r.Activities.Any(a => a.IsStarred && a.UserId == authorId)),
                
                _ => recipes
            };
        }

        public static IQueryable<Recipe> SortBySearchString(this IQueryable<Recipe> recipes, string searchString)
        {
            return searchString == null
                ? recipes
                : recipes.Where(rec =>
                        rec.Name.ToLower().Contains(searchString.ToLower()) ||
                        rec.Tags.Select(tag => tag.Value).Contains(searchString));
        }
    }
}