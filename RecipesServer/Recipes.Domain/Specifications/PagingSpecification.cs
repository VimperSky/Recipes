namespace Recipes.Domain.Specifications
{
    public class PagingSpecification<T>
    {
        public PagingSpecification(int skip, int take)
        {
            Skip = skip;
            Take = take;
        }

        public int Skip { get; }
        public int Take { get; }
    }
}