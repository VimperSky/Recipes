using System;
using System.Linq.Expressions;
using Recipes.Domain.Models;
using Recipes.Domain.Specifications;

namespace Recipes.Application.Services.Recipes.Specifications
{
    public class OwnRecipesFilterSpecification: FilterSpecification<Recipe>
    {
        private readonly int _authorId;

        public OwnRecipesFilterSpecification(int authorId)
        {
            _authorId = authorId;
        }

        public override Expression<Func<Recipe, bool>> SpecificationExpression =>
            recipe => recipe.AuthorId == _authorId;
    }
}