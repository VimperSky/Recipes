using System.Collections.Generic;
using Recipes.Domain.Models;

namespace Recipes.Domain.Repositories
{
    public interface IRecipesRepository
    {
        IEnumerable<Recipe> Get(string searchString, int page);

        Recipe GetById(int id);
    }
}