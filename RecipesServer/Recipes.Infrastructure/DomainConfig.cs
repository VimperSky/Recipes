using Recipes.Domain;

namespace Recipes.Infrastructure
{
    public class DomainConfig: IDomainConfig
    {
        public int DefaultPageSize => 5;
    }
}