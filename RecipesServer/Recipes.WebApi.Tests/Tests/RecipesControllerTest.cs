using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Recipes.WebApi.DTO;
using Recipes.WebApi.Tests.HelpClasses;
using Xunit;

namespace Recipes.WebApi.Tests.Tests
{
    [Collection("Tests")]
    public class RecipesControllerTest: IClassFixture<TestWebFactory<Startup>>
    {
        private readonly HttpClient _client;

        internal RecipesControllerTest(TestWebFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }
        
        [Theory]
        [InlineData("-1")]
        [InlineData("test")]
        internal async Task Get_RecipeList_InvalidPage_ReturnsBadRequest(string page)
        {
            // Act
            var response = await _client.GetAsync($"/recipes/list?page={page}");
            
            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }


        [Fact]
        internal async Task Get_RecipeList_NonExistingPage_ReturnsNotFound()
        {
            // Act
            var response = await _client.GetAsync("/recipes/list?page=5");
            
            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
        
        [Fact]
        internal async Task Get_RecipeList_ExistingPage_ReturnValues()
        {
            // Act
            var response = await _client.GetAsync("/recipes/list?page=1");
            var result = JsonConvert.DeserializeObject<RecipesPage>(await response.Content.ReadAsStringAsync());

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(TestDbCreator.FirstPageRecipes.Length, result.Recipes.Length);
        }

        
        [Fact]
        internal async Task Get_RecipeList_NoPagePassed_ReturnValuesFromFirstPage()
        {
            // Act
            var response = await _client.GetAsync("/recipes/list");
            var result = JsonConvert.DeserializeObject<RecipesPage>(await response.Content.ReadAsStringAsync());

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            CustomAssert.Equal(TestDbCreator.FirstPageRecipes, result.Recipes);
        }
        
        [Fact]
        internal async Task Get_RecipeList_SearchForNonExistingItems_ReturnEmptyRecipeList()
        {
            // Act
            var response = await _client.GetAsync("/recipes/list?searchString=abcdef");
            var result = JsonConvert.DeserializeObject<RecipesPage>(await response.Content.ReadAsStringAsync());

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Empty(result.Recipes);
        }
        
        [Fact]
        internal async Task Get_RecipeList_SearchForExistingItems_ReturnExpectedRecipeList()
        {
            // Arrange
            const string searchString = "па";
            
            // Act
            var response = await _client.GetAsync($"/recipes/list?searchString={searchString}");
            var result = JsonConvert.DeserializeObject<RecipesPage>(await response.Content.ReadAsStringAsync());

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            CustomAssert.Equal(TestDbCreator.FirstPageRecipes.Where(x => x.Name.ToLower().Contains(searchString)).ToList(), result.Recipes);
        }
    }
}