using System.Collections.Generic;
using System.Linq;
using Recipes.Domain.Models;
using Recipes.WebApi.DTO.Recipe;
using Xunit.Sdk;

namespace Recipes.WebApi.Tests.HelpClasses
{
    public static class CustomAssert
    {
        public static void Equal(Recipe recipe, RecipeDetail recipeDetail)
        {
            var equal = ModelComparator.CompareRecipes(recipe, recipeDetail);
            if (!equal)
                throw new EqualException(recipe, recipeDetail);
        }

        private static void Equal(Recipe recipe, RecipePreview recipePreview)
        {
            var equal = ModelComparator.CompareRecipes(recipe, recipePreview);
            if (!equal)
                throw new EqualException(recipe, recipePreview);
        }
        
        public static void Equal(IList<Recipe> recipes, IList<RecipePreview> recipePreviews)
        {
            if (recipes.Count != recipePreviews.Count)
                throw new EqualException(recipes, recipePreviews);

            for (var i = 0; i < recipes.Count; i++)
                Equal(recipes[i], recipePreviews[i]);
        }
    }
}