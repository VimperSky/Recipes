using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Recipes.Domain.Models;
using Recipes.Domain.Repositories;

namespace Recipes.Infrastructure.Repositories
{
    public class RecipesRepository : IRecipesRepository
    {
        private readonly RecipesDbContext _recipesDbContext;

        public RecipesRepository(RecipesDbContext recipesDbContext)
        {
            _recipesDbContext = recipesDbContext;
        }


        public async Task<IEnumerable<Recipe>> GetList(int skipItems, int takeItems, string searchString = default,
            int authorId = default)
        {
            return await _recipesDbContext.Recipes.SortByAuthorId(authorId)
                .SortBySearchString(searchString)
                .OrderBy(x => x.Id)
                .Skip(skipItems)
                .Take(takeItems)
                .Include(x => x.Tags)
                .ToListAsync();
        }

        public async Task<int> GetRecipesCount(string searchString, int authorId)
        {
            return await _recipesDbContext.Recipes.SortByAuthorId(authorId)
                .SortBySearchString(searchString)
                .Include(x => x.Tags)
                .CountAsync();
        }

        public async Task<Recipe> AddRecipe(Recipe recipe)
        {
            if (recipe.Id != default)
                recipe.Id = default;
            
            var addedRecipe = await _recipesDbContext.Recipes.AddAsync(recipe);
            return addedRecipe.Entity;
        }


        public Task DeleteRecipe(Recipe recipe)
        {
            _recipesDbContext.Recipes.Remove(recipe);
            return Task.CompletedTask;
        }

        public async Task<Recipe> GetById(int id)
        {
            return await _recipesDbContext.Recipes
                .Include(x => x.Tags)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}