using Xunit;

namespace Recipes.WebApi.IntegrationTests.Tests
{
    [CollectionDefinition("TestsCollection")]
    public class TestsCollection : ICollectionFixture<TestWebFactory<Startup>>
    {
    }
}