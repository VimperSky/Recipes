using System.Collections.Generic;
using System.Threading.Tasks;
using Recipes.Domain.Models;

namespace Recipes.Domain.Repositories
{
    public interface IRecipesRepository
    {
        Task<IEnumerable<Recipe>> GetList(string searchString, int skipItems, int takeItems);

        Task<int> GetRecipesCount(string searchString);

        Task<Recipe> AddRecipe(Recipe recipe);
        
        Task DeleteRecipe(Recipe recipe);
        
        Task<Recipe> GetById(int id);
    }
}