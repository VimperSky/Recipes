using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json;
using Recipes.Application.DTOs.Recipe;
using Xunit;
using static Recipes.WebApi.IntegrationTests.Tests.RecipeController.DataProviders.RecipeDataProvider;


namespace Recipes.WebApi.IntegrationTests.Tests.RecipeController
{    
    [Collection("TestsCollection")]
    public class EditTests
    {
        private readonly HttpClient _client;
        
        private const string BaseAddress = "api/recipe";

        public EditTests(TestWebFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }
        
        [Fact]
        public async Task Patch_Edit_NoAuthorization_ReturnsUnauthorized()
        {
            // Act
            var response = await _client.PatchAsJsonAsync($"{BaseAddress}/edit", TestRecipeEditDto);

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
        
        [Fact]
        public async Task Patch_Edit_InvalidArguments_ReturnsBadRequest()
        {
            // Arrange
            var copyDto = TestRecipeEditDto;
            copyDto.Name = null;
            _client.SetAuthToken();
            
            // Act
            var response = await _client.PatchAsJsonAsync($"{BaseAddress}/edit", copyDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
        
        [Fact]
        public async Task Patch_Edit_OwnedRecipe_ReturnsOk()
        {
            _client.SetAuthToken();
            var createdRecipe = await _client.PostAsJsonAsync($"{BaseAddress}/create", TestRecipeCreateDto);
            createdRecipe.StatusCode.Should().Be(HttpStatusCode.Created);
            var recipeId = JsonConvert.DeserializeObject<int>(await createdRecipe.Content.ReadAsStringAsync());

            var copyRecipe = TestRecipeEditDto;
            copyRecipe.Id = recipeId;
            
            var editResponse = await _client.PatchAsJsonAsync($"{BaseAddress}/edit", TestRecipeEditDto);
            editResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            
            var detail = await _client.GetFromJsonAsync<RecipeDetailDto>($"{BaseAddress}/detail?id={recipeId}");
            
            detail.Should().BeEquivalentTo(TestRecipeEditDto);
        }
        
        [Fact]
        public async Task Patch_Edit_ForeignRecipe_ReturnsForbidden()
        {
            _client.SetAuthToken();
            var createdRecipe = await _client.PostAsJsonAsync($"{BaseAddress}/create", TestRecipeCreateDto);
            createdRecipe.StatusCode.Should().Be(HttpStatusCode.Created);
            var recipeId = JsonConvert.DeserializeObject<int>(await createdRecipe.Content.ReadAsStringAsync());

            _client.SetAuthToken(true);
            var copyRecipe = TestRecipeEditDto;
            copyRecipe.Id = recipeId;
            var editResponse = await _client.PatchAsJsonAsync($"{BaseAddress}/edit", TestRecipeEditDto);
            editResponse.StatusCode.Should().Be(HttpStatusCode.Forbidden);
            
            var detail = await _client.GetFromJsonAsync<RecipeDetailDto>($"{BaseAddress}/detail?id={recipeId}");
            
            detail.Should().BeEquivalentTo(TestRecipeCreateDto);
        }
    }
}