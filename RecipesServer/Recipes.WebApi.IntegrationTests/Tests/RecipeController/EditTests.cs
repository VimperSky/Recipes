using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json;
using Recipes.WebApi.DTOs.Recipe;
using Xunit;
using static Recipes.WebApi.IntegrationTests.Tests.RecipeController.DataProviders.RecipeDataProvider;


namespace Recipes.WebApi.IntegrationTests.Tests.RecipeController
{
    [Collection("TestsCollection")]
    public class EditTests
    {
        private const string BaseAddress = "api/recipe";
        private readonly HttpClient _client;

        public EditTests(TestWebFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Patch_Edit_NoAuthorization_ReturnsUnauthorized()
        {
            // Act
            var response = await _client.PatchAsJsonAsync($"{BaseAddress}/edit", TestRecipeEditRequestDTO);

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task Patch_Edit_InvalidArguments_ReturnsBadRequest()
        {
            // Arrange
            var copyDto = TestRecipeEditRequestDTO;
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
            // Arrange
            _client.SetAuthToken();
            var createdRecipe = await _client.PostAsJsonAsync($"{BaseAddress}/create", TestRecipeCreateRequestDTO);
            createdRecipe.StatusCode.Should().Be(HttpStatusCode.Created);
            var recipeId = JsonConvert.DeserializeObject<int>(await createdRecipe.Content.ReadAsStringAsync());

            var copyRecipe = TestRecipeEditRequestDTO;
            copyRecipe.Id = recipeId;

            // Act && Assert
            var editResponse = await _client.PatchAsJsonAsync($"{BaseAddress}/edit", copyRecipe);
            editResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var detail = await _client.GetFromJsonAsync<RecipeDetailResultResultDTO>($"{BaseAddress}/detail?id={recipeId}");

            detail.Should().BeEquivalentTo(copyRecipe);
        }

        [Fact]
        public async Task Patch_Edit_ForeignRecipe_ReturnsForbidden()
        {
            // Arrange
            _client.SetAuthToken();
            var createdRecipe = await _client.PostAsJsonAsync($"{BaseAddress}/create", TestRecipeCreateRequestDTO);
            createdRecipe.StatusCode.Should().Be(HttpStatusCode.Created);
            var recipeId = JsonConvert.DeserializeObject<int>(await createdRecipe.Content.ReadAsStringAsync());

            
            _client.SetAuthToken(true);
            var copyRecipe = TestRecipeEditRequestDTO;
            copyRecipe.Id = recipeId;

            // Act && Assert
            var editResponse = await _client.PatchAsJsonAsync($"{BaseAddress}/edit", copyRecipe);
            editResponse.StatusCode.Should().Be(HttpStatusCode.Forbidden);

            var detail = await _client.GetFromJsonAsync<RecipeDetailResultResultDTO>($"{BaseAddress}/detail?id={recipeId}");

            detail.Should().BeEquivalentTo(copyRecipe);
        }
    }
}