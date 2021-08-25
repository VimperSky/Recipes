using System.Collections.Generic;
using System.Threading.Tasks;
using Recipes.Domain.Models;

namespace Recipes.Domain.Repositories
{
    public interface IRecipesRepository
    {
        Task<List<Recipe>> GetList(int skipItems, int takeItems, string searchString,
            RecipesType recipesType, int authorId);
 
        Task<int> GetRecipesCount(string searchString, 
            RecipesType recipesType, int authorId);

        Task<Recipe> AddRecipe(Recipe recipe);

        Task DeleteRecipe(Recipe recipe);

        Task<Recipe> GetById(int id);

        Task<Recipe> GetRecipeOfTheDay();
    }
}