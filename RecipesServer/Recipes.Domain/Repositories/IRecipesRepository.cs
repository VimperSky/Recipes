using System.Collections.Generic;
using Recipes.Domain.Models;

namespace Recipes.Domain.Repositories
{
    public interface IRecipesRepository
    {
        IEnumerable<Recipe> Get(string searchString, int skipItems, int takeItems);

        int GetRecipesCount(string searchString);

        int AddRecipe(Recipe recipe);

        void EditRecipe(Recipe recipe);

        void DeleteRecipe(int id);
        
        Recipe GetById(int id);
    }
}