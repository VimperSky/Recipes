using System.Net.Http;

namespace Recipes.WebApi.IntegrationTests.Tests.AuthController
{
    public class DeleteTests
    {        
        private readonly HttpClient _client;

        public DeleteTests(TestWebFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

    }
}