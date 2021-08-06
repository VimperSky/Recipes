using System.Collections.Generic;
using Recipes.Domain.Models;

namespace Recipes.Domain.Repositories
{
    public interface IRecipesRepository
    {
        IEnumerable<Recipe> GetPage(int page, int pageSize, string searchString);

        int GetPagesCount(int pageSize, string searchString);

        int AddRecipe(Recipe recipe);

        void EditRecipe(Recipe recipe);

        void DeleteRecipe(int id);
        
        Recipe GetById(int id);
    }
}