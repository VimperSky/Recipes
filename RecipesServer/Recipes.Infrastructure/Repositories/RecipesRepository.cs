using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Recipes.Domain.Models;
using Recipes.Domain.Repositories;

namespace Recipes.Infrastructure.Repositories
{
    public class RecipesRepository: IRecipesRepository
    {
        private readonly RecipesDbContext _recipesDbContext;

        public RecipesRepository(RecipesDbContext recipesDbContext)
        {
            _recipesDbContext = recipesDbContext;
        }

        private static IQueryable<Recipe> SortBySearchString(IQueryable<Recipe> recipes, string searchString)
        {
            return searchString == null ? recipes : 
                recipes.Where(x => x.Name.ToLower().Contains(searchString.ToLower()));
        }
        
        public IEnumerable<Recipe> Get(string searchString, int skipItems, int takeItems)
        {
            return SortBySearchString(_recipesDbContext.Recipes, searchString)
                .OrderBy(x => x.Id)
                .Skip(skipItems)
                .Take(takeItems)
                .Include(x => x.IngredientBlocks)
                .ToList();
        }

        public int GetRecipesCount(string searchString)
        {
            IQueryable<Recipe> result = _recipesDbContext.Recipes;

            // Сортируем по поисковой строке
            result = SortBySearchString(result, searchString);
            return result.Count();
        }

        public int AddRecipe(Recipe recipe)
        {
            if (recipe.Id != default)
            {
                throw new ArgumentException("Cannot add recipe with predefined ID.");
            }
            var addedRecipe = _recipesDbContext.Recipes.Add(recipe);
            return addedRecipe.Entity.Id;
        }

        public void EditRecipe(Recipe recipe)
        {
            var dbRecipe = _recipesDbContext.Recipes.Find(recipe.Id);
            if (dbRecipe == null)
                throw new ArgumentException("Couldn't edit recipe because recipe with id: " +
                                            $"{recipe.Id} doesn't exist");

            _recipesDbContext.Recipes.Update(recipe);
        }

        public void DeleteRecipe(int id)
        {
            var dbRecipe = _recipesDbContext.Recipes.Find(id);
            if (dbRecipe == null)
                throw new ArgumentException($"Cannot delete recipe with id: {id} because it doesn't exist in database");

            _recipesDbContext.Recipes.Remove(dbRecipe);
        }

        public Recipe GetById(int id)
        {
            return _recipesDbContext.Recipes.Find(id);
        }
        
        
    }
}