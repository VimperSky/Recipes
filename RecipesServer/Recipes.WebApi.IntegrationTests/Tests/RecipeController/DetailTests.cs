using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json;
using Recipes.Application.DTOs.Recipe;
using Recipes.WebApi.IntegrationTests.TestDbProviders;
using Xunit;

namespace Recipes.WebApi.IntegrationTests.Tests.RecipeController
{
    public class DetailTests: IClassFixture<TestWebFactory<Startup>>
    {
        private readonly HttpClient _client;
        private readonly TestRecipesDbProvider _recipesDbProvider;

        private const string BaseAddress = "api/recipe";
        
        public DetailTests(TestWebFactory<Startup> factory)
        {
            _client = factory.CreateClient();

            _recipesDbProvider = new TestRecipesDbProvider();
        }
        
        [Theory]
        [InlineData("-1")]
        [InlineData("test")]
        public async Task Get_Detail_InvalidRecipeId_ReturnsBadRequest(string id)
        {
            // Act
            var response = await _client.GetAsync($"{BaseAddress}/detail?id={id}");
            
            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
        
        [Fact]
        public async Task Get_Detail_NonExistingRecipeId_ReturnsNotFound()
        {
            // Act
            var response = await _client.GetAsync($"{BaseAddress}/detail?id=555");
            
            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
        
        [Fact]
        public async Task Get_Detail_ExistingRecipeId_ReturnsValue()
        {
            // Arrange
            const int recipeId = 1;

            // Act
            var response = await _client.GetAsync($"{BaseAddress}/detail?id={recipeId}");
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<RecipeDetailDto>(content);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            result.Should().BeEquivalentTo(_recipesDbProvider.Detail(recipeId));
        }
        
        [Fact]
        public async Task Get_Detail_RecipeIdNotPassed_ReturnsBadRequest()
        {
            // Act
            var response = await _client.GetAsync($"{BaseAddress}/detail");

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}