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
    }
}