using Recipes.Domain;

namespace Recipes.WebApi.Tests.HelpClasses
{
    public class TestDomainConfig: IDomainConfig
    {
        public int PageSize => 5;
    }
}