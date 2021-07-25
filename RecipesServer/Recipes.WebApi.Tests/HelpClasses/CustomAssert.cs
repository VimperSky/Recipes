using System.Collections.Generic;
using System.Linq;
using Recipes.Domain.Models;
using Recipes.WebApi.DTO.Recipe;
using Xunit.Sdk;

namespace Recipes.WebApi.Tests.HelpClasses
{
    public static class CustomAssert
    {
        public static void Equal(RecipeDetailDto expected, RecipeDetailDto actual)
        {
            var equal = ModelComparator.CompareRecipes(expected, actual);
            if (!equal)
                throw new EqualException(expected, actual);
        }

        private static void Equal(RecipePreviewDto expected, RecipePreviewDto actual)
        {
            var equal = ModelComparator.CompareRecipes(expected, actual);
            if (!equal)
                throw new EqualException(expected, actual);
        }
        
        public static void Equal(IList<RecipePreviewDto> expected, IList<RecipePreviewDto> actual)
        {
            if (expected.Count != actual.Count)
                throw new EqualException(expected, actual);

            for (var i = 0; i < expected.Count; i++)
                Equal(expected[i], actual[i]);
        }
    }
}