using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit;
using static Recipes.WebApi.IntegrationTests.Tests.RecipeController.DataProviders.RecipeDataProvider;

namespace Recipes.WebApi.IntegrationTests.Tests.RecipeController
{    
    [Collection("TestsCollection")]
    public class DeleteTests
    {
        private const string BaseAddress = "api/recipe";

        private readonly HttpClient _client;
        
        public DeleteTests(TestWebFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }
        
        [Fact]
        public async Task Delete_NoIdPassed_ReturnsBadRequest()
        {
            // Act
            var response = await _client.DeleteAsync($"{BaseAddress}/delete");

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
        
        [Fact]
        public async Task Delete_NoAuthorization_ReturnsUnauthorized()
        {
            // Act
            var response = await _client.DeleteAsync($"{BaseAddress}/delete?id=1");

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
        
        [Fact]
        public async Task Delete_NotOwnedRecipe_ReturnsForbidden()
        {
            _client.SetAuthToken();
            
            // Act
            var response = await _client.DeleteAsync($"{BaseAddress}/delete?id=1");

            // Assert
            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }
        
        [Fact]
        public async Task Delete_NonExistingRecipe_ReturnsNotFound()
        {
            _client.SetAuthToken();
            
            // Act
            var response = await _client.DeleteAsync($"{BaseAddress}/delete?id=777");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
        
        [Fact]
        public async Task Delete_OwnedRecipe_ReturnsOk()
        {
            _client.SetAuthToken();
            // Arrange
            var createdRecipe = await _client.PostAsJsonAsync($"{BaseAddress}/create", TestRecipeCreateDto);
            createdRecipe.StatusCode.Should().Be(HttpStatusCode.Created);
            var recipeId = JsonConvert.DeserializeObject<int>(await createdRecipe.Content.ReadAsStringAsync());

            // Act
            var response = await _client.DeleteAsync($"{BaseAddress}/delete?id={recipeId}");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}