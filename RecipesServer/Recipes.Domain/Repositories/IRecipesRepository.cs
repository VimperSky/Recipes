using System.Collections.Generic;
using System.Threading.Tasks;
using Recipes.Domain.Models;

namespace Recipes.Domain.Repositories
{
    public interface IRecipesRepository
    {
        Task<IEnumerable<Recipe>> Get(string searchString, int skipItems, int takeItems);

        Task<int> GetRecipesCount(string searchString);

        Task<Recipe> AddRecipe(Recipe recipe);

        Task EditRecipe(Recipe recipe);

        Task DeleteRecipe(int id);
        
        Task<Recipe> GetById(int id);
    }
}