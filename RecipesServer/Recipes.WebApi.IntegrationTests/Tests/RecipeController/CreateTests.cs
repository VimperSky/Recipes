using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json;
using Recipes.WebApi.DTOs.Recipe;
using Xunit;
using static Recipes.WebApi.IntegrationTests.Tests.RecipeController.DataProviders.RecipeDataProvider;

namespace Recipes.WebApi.IntegrationTests.Tests.RecipeController
{
    [Collection("TestsCollection")]
    public class CreateTests
    {
        private const string BaseAddress = "api/recipe";
        private readonly HttpClient _client;


        public CreateTests(TestWebFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Post_Create_NoAuthorization_ReturnsUnauthorized()
        {
            // Act
            var response = await _client.PostAsJsonAsync($"{BaseAddress}/create", TestRecipeCreateRequestDTO);

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task Post_Create_InvalidArguments_ReturnsBadRequest()
        {
            // Arrange
            var copyDto = TestRecipeCreateRequestDTO;
            copyDto.Name = null;
            _client.SetAuthToken();

            // Act
            var response = await _client.PostAsJsonAsync($"{BaseAddress}/create", copyDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }


        [Fact]
        public async Task Post_Create_Authorized_ReturnsCreated()
        {
            // Arrange
            _client.SetAuthToken();

            // Act && Assert
            var createdRecipe = await _client.PostAsJsonAsync($"{BaseAddress}/create", TestRecipeCreateRequestDTO);
            createdRecipe.StatusCode.Should().Be(HttpStatusCode.Created);

            var detail = await _client.GetFromJsonAsync<RecipeDetailResultDTO>($"{BaseAddress}/detail?id=" +
                                                                         JsonConvert.DeserializeObject<int>(
                                                                             await createdRecipe.Content
                                                                                 .ReadAsStringAsync()));

            detail.Should().BeEquivalentTo(TestRecipeCreateRequestDTO);
        }
    }
}