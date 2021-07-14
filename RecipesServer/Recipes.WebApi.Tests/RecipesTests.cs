using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Recipes.WebApi.Tests
{
    public class RecipesTests: IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public RecipesTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }
    }
}