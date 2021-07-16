using System.Collections.Generic;
using System.Linq;
using Recipes.Domain.Models;
using Recipes.WebApi.DTO.Recipe;

namespace Recipes.WebApi.Tests.HelpClasses
{
    public static class ModelComparator
    {
        private static bool CompareIngredients(Ingredient a, Ingredient b)
        {
            return a.Header == b.Header && a.Value == b.Value;
        }
        
        private static bool CompareIngredientLists(IReadOnlyList<Ingredient> a, IReadOnlyList<Ingredient> b)
        {
            if (a.Count != b.Count)
                return false;

            return !a.Where((t, i) => !CompareIngredients(t, b[i])).Any();
        }
        
        public static bool CompareRecipes(Recipe recipe, RecipeDetail recipeDetail)
        {
            return recipe.Id == recipeDetail.Id 
                   && recipe.Name == recipeDetail.Name
                   && recipe.Description == recipeDetail.Description 
                   && recipe.Portions == recipeDetail.Portions
                   && recipe.CookingTimeMin == recipeDetail.CookingTime
                   && recipe.Steps.SequenceEqual(recipeDetail.Steps)
                   && CompareIngredientLists(recipe.IngredientBlocks.Select(Ingredient.FromModel).ToArray(), 
                       recipeDetail.Ingredients);
        }
        
    }
}