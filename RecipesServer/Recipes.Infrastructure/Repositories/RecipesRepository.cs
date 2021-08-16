﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        
        
        public async Task<IEnumerable<Recipe>> GetList(string searchString, int skipItems, int takeItems)
        {
            return await SortBySearchString(_recipesDbContext.Recipes, searchString)
                .OrderBy(x => x.Id)
                .Skip(skipItems)
                .Take(takeItems)
                .Include(x => x.Ingredients)
                .ToListAsync();
        }

        public async Task<int> GetRecipesCount(string searchString)
        {
            // Сортируем по поисковой строке
            return await SortBySearchString(_recipesDbContext.Recipes, searchString)
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
            return await _recipesDbContext.Recipes.FindAsync(id);
        }
        
        private static IQueryable<Recipe> SortBySearchString(IQueryable<Recipe> recipes, string searchString)
        {
            return searchString == null ? recipes : 
                recipes.Where(x => x.Name.ToLower().Contains(searchString.ToLower()));
        }

    }
}