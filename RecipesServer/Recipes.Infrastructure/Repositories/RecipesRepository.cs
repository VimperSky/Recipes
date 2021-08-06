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

        private IQueryable<Recipe> SortBySearchString(IQueryable<Recipe> recipes, string searchString)
        {
            return searchString == null ? recipes : 
                recipes.Where(x => x.Name.ToLower().Contains(searchString.ToLower()));
        }
        
        public int GetPagesCount(int pageSize, string searchString)
        {
            if (pageSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(pageSize));
            
            IQueryable<Recipe> result = _recipesDbContext.Recipes;

            // Сортируем по поисковой строке
            result = SortBySearchString(result, searchString);
            
            return (int)Math.Ceiling(result.Count() * 1d / pageSize);
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

        IEnumerable<Recipe> IRecipesRepository.GetPage(int page, int pageSize, string searchString)
        {
            if (pageSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(pageSize));
            
            if (page <= 0)
                throw new ArgumentOutOfRangeException(nameof(page));

            return SortBySearchString(_recipesDbContext.Recipes, searchString)
                .OrderBy(x => x.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Include(x => x.IngredientBlocks)
                .ToList();
        }

        public Recipe GetById(int id)
        {
            return _recipesDbContext.Recipes.Find(id);
        }
        
        
    }
}