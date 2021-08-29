using System;
using System.Linq;
using System.Linq.Expressions;
using Recipes.Domain.Models;
using Recipes.Domain.Specifications;

namespace Recipes.Application.Services.Recipes.Specifications
{
    public class AllRecipesFilterSpecification : FilterSpecification<Recipe>
    {
        private readonly string _searchString;

        public AllRecipesFilterSpecification(string searchString)
        {
            _searchString = searchString;
        }

        public override Expression<Func<Recipe, bool>> SpecificationExpression => recipe =>
            _searchString == null ||
            recipe.Name.ToLower().Contains(_searchString.ToLower()) ||
            recipe.Tags.Select(tag => tag.Value).Contains(_searchString);
    }
}