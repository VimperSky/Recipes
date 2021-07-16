using System.Collections.Generic;
using Recipes.Domain.Models;

namespace Recipes.Domain.Repositories
{
    public interface IRecipesRepository
    {
        (IEnumerable<Recipe> Values, bool HasMore) GetPage(int page, string searchString);

        Recipe GetById(int id);
    }
}