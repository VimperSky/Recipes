using System;
using System.Linq.Expressions;

namespace Recipes.Domain.Specifications
{
    public abstract class FilterSpecification<T>
    {
        public abstract Expression<Func<T, bool>> SpecificationExpression { get; }
    }
}