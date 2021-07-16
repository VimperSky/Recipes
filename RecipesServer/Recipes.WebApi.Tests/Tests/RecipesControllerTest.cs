using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Recipes.WebApi.DTO.Recipe;
using Recipes.WebApi.Tests.HelpClasses;
using Xunit;

namespace Recipes.WebApi.Tests.Tests
{
    public class RecipesControllerTest: IClassFixture<TestWebFactory<Startup>>
    {
        private readonly HttpClient _client;

        public RecipesControllerTest(TestWebFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }
        
        [Theory]
        [InlineData("-1")]
        [InlineData("test")]
        private async Task Get_RecipeList_InvalidPage_ReturnsBadRequest(string page)
        {
            // Act
            var response = await _client.GetAsync($"/recipes/list?page={page}");
            
            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }


        [Fact]
        private async Task Get_RecipeList_NonExistingPage_ReturnsNotFound()
        {
            // Act
            var response = await _client.GetAsync("/recipes/list?page=5");
            
            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
        
        [Fact]
        private async Task Get_RecipeList_ExistingPage_ReturnValues()
        {
            // Act
            var response = await _client.GetAsync("/recipes/list?page=1");
            var result = JsonConvert.DeserializeObject<RecipePreview[]>(await response.Content.ReadAsStringAsync());

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(TestDbCreator.FirstPageRecipes.Length, result.Length);
        }

        
        [Fact]
        private async Task Get_RecipeList_NoPagePassed_ReturnValuesFromFirstPage()
        {
            // Act
            var response = await _client.GetAsync("/recipes/list");
            var result = JsonConvert.DeserializeObject<RecipePreview[]>(await response.Content.ReadAsStringAsync());

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(TestDbCreator.FirstPageRecipes.Length, result.Length);
        }
    }
}