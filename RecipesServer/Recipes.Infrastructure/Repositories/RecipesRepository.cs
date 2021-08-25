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
        
        public async Task<Recipe> GetById(int id)
        {
            return await _recipesDbContext.Recipes
                .Include(x => x.Tags)
                .Include(x => x.Author)
                .Include(x => x.Activities)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Recipe> GetRecipeOfTheDay()
        {
            var recipeId = await _recipesDbContext.Activities
                .AsNoTracking()
                .Where(x => x.IsLiked)
                .GroupBy(x => x.RecipeId)
                .Select(x => new {x.Key, Count = x.Count()})
                .OrderBy(x => x.Count)
                .FirstOrDefaultAsync();

            if (recipeId == null)
                return null;
            
            var recipe = await _recipesDbContext.Recipes.FindAsync(recipeId.Key);
            await _recipesDbContext.Entry(recipe).Collection(x => x.Activities).LoadAsync();
            return recipe;
        }

        public async Task<List<Recipe>> GetList(int skipItems, int takeItems, string searchString,
            RecipesType recipesType, int authorId)
        {
            return await _recipesDbContext.Recipes
                .AsNoTracking()
                .SortByType(recipesType, authorId)
                .SortBySearchString(searchString)
                .OrderBy(x => x.Id)
                .Skip(skipItems)
                .Take(takeItems)
                .Include(x => x.Tags)
                .Include(x => x.Author)
                .Include(x => x.Activities)
                .ToListAsync();
        }

        public async Task<int> GetRecipesCount(string searchString, RecipesType recipesType, int authorId)
        {
            return await _recipesDbContext.Recipes
                .AsNoTracking()
                .SortByType(recipesType, authorId)
                .SortBySearchString(searchString)
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
    }
}