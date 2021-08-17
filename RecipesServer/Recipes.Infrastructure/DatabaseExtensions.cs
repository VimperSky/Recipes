using System.Linq;
using Recipes.Domain.Models;

namespace Recipes.Infrastructure
{
    public static class DatabaseExtensions
    {
        public static IQueryable<Recipe> SortByAuthorId(this IQueryable<Recipe> recipes, int authorId)
        {
            return authorId == default
                ? recipes
                : recipes.Where(x => x.AuthorId == authorId);
        }

        public static IQueryable<Recipe> SortBySearchString(this IQueryable<Recipe> recipes, string searchString)
        {
            return searchString == null
                ? recipes
                : recipes.Where(x => x.Name.ToLower().Contains(searchString.ToLower()));
        }
    }
}