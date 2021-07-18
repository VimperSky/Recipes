using System.Collections.Generic;
using Recipes.Domain.Models;

namespace Recipes.Domain.Repositories
{
    public interface IRecipesRepository
    {
        IList<Recipe> GetPage(int page, int pageSize, string searchString);

        int GetPagesCount(int pageSize, string searchString);
        
        Recipe GetById(int id);
    }
}