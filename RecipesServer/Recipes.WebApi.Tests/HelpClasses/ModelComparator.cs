using System.Collections.Generic;
using System.Linq;
using Recipes.Domain.Models;
using Recipes.WebApi.DTO.Recipe;

namespace Recipes.WebApi.Tests.HelpClasses
{
    public static class ModelComparator
    {
        private static bool CompareIngredients(IngredientDto a, IngredientDto b)
        {
            return a.Header == b.Header && a.Value == b.Value;
        }
        
        private static bool CompareIngredientLists(IReadOnlyCollection<IngredientDto> a, IReadOnlyList<IngredientDto> b)
        {
            if (a.Count != b.Count)
                return false;

            return !a.Where((t, i) => !CompareIngredients(t, b[i])).Any();
        }
        
        public static bool CompareRecipes(RecipeDetailDto a, RecipeDetailDto b)
        {
            return a.Id == b.Id 
                   && a.Name == b.Name
                   && a.Description == b.Description 
                   && a.Portions == b.Portions
                   && a.CookingTimeMin == b.CookingTimeMin
                   && a.Steps.SequenceEqual(b.Steps)
                   && CompareIngredientLists(a.Ingredients, b.Ingredients);
        }
        
        
        public static bool CompareRecipes(RecipePreviewDto a, RecipePreviewDto b)
        {
            return a.Id == b.Id
                   && a.Name == b.Name
                   && a.Description == b.Description
                   && a.Portions == b.Portions
                   && a.CookingTimeMin == b.CookingTimeMin;
        }
    }
}