using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Recipes.Domain;
using Recipes.Domain.Models;
using Recipes.Domain.Repositories;

namespace Recipes.Infrastructure.Repositories
{
    public class RecipesRepository: IRecipesRepository
    {
        private readonly RecipesContext _recipesContext;
        private readonly IDomainConfig _domainConfig;

        public RecipesRepository(RecipesContext recipesContext, IDomainConfig domainConfig)
        {
            _recipesContext = recipesContext;
            _domainConfig = domainConfig;
        }

        private IQueryable<Recipe> SortBySearchString(IQueryable<Recipe> recipes, string searchString)
        {
            return searchString == null ? recipes : 
                recipes.Where(x => x.Name.ToLower().Contains(searchString.ToLower()));
        }
        
        public int GetPagesCount(int pageSize, string searchString)
        {
            if (pageSize <= 0)
                pageSize = _domainConfig.DefaultPageSize;
            
            IQueryable<Recipe> result = _recipesContext.Recipes;

            // Сортируем по поисковой строке
            result = SortBySearchString(result, searchString);
            
            return (int)Math.Ceiling(result.Count() * 1d / pageSize);
        }

        IList<Recipe> IRecipesRepository.GetPage(int page, int pageSize, string searchString)
        {
            if (page <= 0)
                page = 1;
            
            if (pageSize <= 0)
                pageSize = _domainConfig.DefaultPageSize;
            
            return SortBySearchString(_recipesContext.Recipes, searchString)
                .OrderBy(x => x.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Include(x => x.IngredientBlocks)
                .ToList();
        }

        public Recipe GetById(int id)
        {
            return _recipesContext.Recipes.Find(id);
        }
    }
}