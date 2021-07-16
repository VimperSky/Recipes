using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Recipes.WebApi.DTO.Recipe;
using Recipes.WebApi.Tests.HelpClasses;
using Xunit;

namespace Recipes.WebApi.Tests.Tests
{
 public class RecipeControllerTest: IClassFixture<TestWebFactory<Startup>>
    {
        private readonly HttpClient _client;

        public RecipeControllerTest(TestWebFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }
        
        [Theory]
        [InlineData("-1")]
        [InlineData("test")]
        private async Task Get_Detail_InvalidRecipeId_ReturnsBadRequest(string id)
        {
            // Act
            var response = await _client.GetAsync($"/recipe/detail?id={id}");
            
            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
        
        [Fact]
        private async Task Get_Detail_NonExistingRecipeId_ReturnsNotFound()
        {
            // Act
            var response = await _client.GetAsync("/recipe/detail?id=555");
            
            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
        
        [Fact]
        private async Task Get_Detail_ExistingRecipeId_ReturnsValue()
        {
            // Act
            var response = await _client.GetAsync("/recipe/detail?id=2");
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<RecipeDetail>(content);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(RecipeDetail.FromModel(TestDbCreator.FirstPageRecipes[1]), result);
        }
    }
}