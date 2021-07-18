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
        
        public (IEnumerable<Recipe> Values, bool HasMore) GetPage(int page, int pageSize, string searchString)
        {
            if (page <= 0)
                page = 1;
            
            if (pageSize <= 0)
                pageSize = _domainConfig.DefaultPageSize;
            
            IQueryable<Recipe> result = _recipesContext.Recipes;
            
            // Сортируем по поисковой строке
            if (searchString != null)
                result = result.Where(x => x.Name.Contains(searchString));
            
            // Скипаем элементы до текущей страницы
            result = result.OrderBy(x => x.Id).Skip((page - 1) * pageSize);
            
            var hasMore = false;
            // Берем либо кол-во элементов на странице, либо сколько осталось
            if (result.Count() > pageSize)
            {
                result = result.Take(pageSize);
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