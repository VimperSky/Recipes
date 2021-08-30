using System;
using System.Linq;
using System.Linq.Expressions;
using Recipes.Domain.Models;
using Recipes.Domain.Specifications;

namespace Recipes.Application.Services.Recipes.Specifications
{
    public class StarredRecipesFilterSpecification : FilterSpecification<Recipe>
    {
        private readonly int _authorId;

        public StarredRecipesFilterSpecification(int authorId)
        {
            _authorId = authorId;
        }

        public override Expression<Func<Recipe, bool>> SpecificationExpression => r
            => r.Activities.Any(a => a.IsStarred && a.UserId == _authorId);
    }
}