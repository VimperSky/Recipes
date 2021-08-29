using System.Collections.Generic;
using System.Threading.Tasks;
using Recipes.Domain.Models;
using Recipes.Domain.Specifications;

namespace Recipes.Domain.Repositories
{
    public interface IRecipesRepository
    {
        Task<List<Recipe>> GetList(FilterSpecification<Recipe> filterSpecification,
            PagingSpecification<Recipe> pagingSpecification);

        Task<int> GetRecipesCount(FilterSpecification<Recipe> filterSpecification);

        Task<Recipe> AddRecipe(Recipe recipe);

        Task DeleteRecipe(Recipe recipe);

        Task<Recipe> GetById(int id);

        Task<Recipe> GetRecipeOfTheDay();
    }
}