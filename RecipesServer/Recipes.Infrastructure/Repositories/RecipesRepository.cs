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
        
        public (IEnumerable<Recipe> Values, bool HasMore) GetPage(int page, string searchString)
        {
            IQueryable<Recipe> result = _recipesContext.Recipes;

            // Сортируем по поисковой строке
            if (searchString != null)
                result = result.Where(x => x.Name.ToLower().Contains(searchString.ToLower()));
            
            // Скипаем элементы до текущей страницы
            var skipElements = (page - 1) * _domainConfig.PageSize;
            if (skipElements > result.Count())
            {
                return (null, false);
            }
            result = result.OrderBy(x => x.Id).Skip(skipElements);
            
            var hasMore = false;
            // Берем либо кол-во элементов на странице, либо сколько осталось
            if (result.Count() > _domainConfig.PageSize)
            {
                result = result.OrderBy(x => x.Id).Take(_domainConfig.PageSize);
                hasMore = true;
            }

            result = result.Include(x => x.IngredientBlocks);

            return (result, hasMore);
        }

        public Recipe GetById(int id)
        {
            return _recipesContext.Recipes.Find(id);
        }
    }
}