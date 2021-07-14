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

        public RecipesRepository(RecipesContext recipesContext)
        {
            _recipesContext = recipesContext;
        }
        
        public IEnumerable<Recipe> Get(string searchString, int page)
        {
            IQueryable<Recipe> result = _recipesContext.Recipes;
            
            // Сортируем по поисковой строке
            if (searchString != null)
                result = result.Where(x => x.Name.Contains(searchString));
            
            // Скипаем элементы до текущей страницы
            var skipElements = (page - 1) * Constants.PageSize;
            if (skipElements >= result.Count())
            {
                return null;
            }
            result = result.Skip(skipElements);
            
            // Берем либо кол-во элементов на странице, либо сколько осталось
            if (result.Count() > Constants.PageSize)
            {
                result = result.Take(Constants.PageSize);
            }

            result = result.Include(x => x.IngredientBlocks);

            return result;
        }

        public Recipe GetById(int id)
        {
            return _recipesContext.Recipes.Find(id);
        }
    }
}