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
        
        public IEnumerable<Recipe> Get(string searchString, int page)
        {
            IQueryable<Recipe> result = _recipesContext.Recipes;
            
            // Сортируем по поисковой строке
            if (searchString != null)
                result = result.Where(x => x.Name.Contains(searchString));
            
            // Скипаем элементы до текущей страницы
            var skipElements = ((int)page - 1) * _domainConfig.PageSize;
            if (skipElements >= result.Count())
            {
                return null;
            }
            result = result.Skip(skipElements);
            
            // Берем либо кол-во элементов на странице, либо сколько осталось
            if (result.Count() > _domainConfig.PageSize)
            {
                result = result.Take(_domainConfig.PageSize);
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