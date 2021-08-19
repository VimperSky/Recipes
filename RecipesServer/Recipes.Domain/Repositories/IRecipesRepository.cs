using System.Collections.Generic;
using System.Threading.Tasks;
using Recipes.Domain.Models;

namespace Recipes.Domain.Repositories
{
    public interface IRecipesRepository
    {
        Task<IEnumerable<Recipe>> GetList(int skipItems, int takeItems, string searchString = default, int authorId = default);
 
        Task<int> GetRecipesCount(string searchString, int authorId = default);

        Task<Recipe> AddRecipe(Recipe recipe);

        Task DeleteRecipe(Recipe recipe);

        Task<Recipe> GetById(int id);
    }
}