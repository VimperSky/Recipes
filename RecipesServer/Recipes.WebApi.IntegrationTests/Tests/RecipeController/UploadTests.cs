using System.IO;
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
    public class UploadTests
    {
        private const string BaseAddress = "api/recipe";
        private readonly HttpClient _client;

        public UploadTests(TestWebFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Put_Upload_NoRecipeIdPassed_ReturnsBadRequest()
        {
            // Arrange
            _client.SetAuthToken();

            var formData = new MultipartFormDataContent();
            formData.Add(TestImageStream, "file", Path.GetFileName(TestImagePath));

            // Act
            var response = await _client.PutAsync($"{BaseAddress}/uploadImage", formData);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }


        [Fact]
        public async Task Put_Upload_NoFilePassed_ReturnsBadRequest()
        {
            // Arrange
            _client.SetAuthToken();

            var formData = new MultipartFormDataContent();
            formData.Add(new StringContent(1.ToString()), "recipeId");

            // Act
            var response = await _client.PutAsync($"{BaseAddress}/uploadImage", formData);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }


        [Fact]
        public async Task Put_Upload_NoAuthorization_ReturnsUnauthorized()
        {
            // Arrange
            var formData = new MultipartFormDataContent();
            formData.Add(new StringContent(1.ToString()), "recipeId");
            formData.Add(TestImageStream, "file", Path.GetFileName(TestImagePath));

            // Act
            var response = await _client.PutAsync($"{BaseAddress}/uploadImage", formData);

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task Put_Upload_NotExistingRecipeId_ReturnsNotFound()
        {
            // Arrange
            _client.SetAuthToken();
            var formData = new MultipartFormDataContent();
            formData.Add(new StringContent(999.ToString()), "recipeId");
            formData.Add(TestImageStream, "file", Path.GetFileName(TestImagePath));

            // Act
            var response = await _client.PutAsync($"{BaseAddress}/uploadImage", formData);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Put_Upload_NotOwnedRecipe_ReturnsForbidden()
        {
            // Arrange
            _client.SetAuthToken();
            var formData = new MultipartFormDataContent();
            formData.Add(new StringContent(1.ToString()), "recipeId");
            formData.Add(TestImageStream, "file", Path.GetFileName(TestImagePath));

            // Act
            var response = await _client.PutAsync($"{BaseAddress}/uploadImage", formData);

            // Assert
            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        [Fact]
        public async Task Put_Upload_OwnedRecipe_ReturnsOk()
        {
            // Arrange
            _client.SetAuthToken();
            var createdRecipe = await _client.PostAsJsonAsync($"{BaseAddress}/create", TestRecipeCreateRequestDTO);
            createdRecipe.StatusCode.Should().Be(HttpStatusCode.Created);
            var recipeId = JsonConvert.DeserializeObject<int>(await createdRecipe.Content.ReadAsStringAsync());

            var formData = new MultipartFormDataContent();
            formData.Add(new StringContent(recipeId.ToString()), "recipeId");
            formData.Add(TestImageStream, "file", Path.GetFileName(TestImagePath));

            // Act
            var response = await _client.PutAsync($"{BaseAddress}/uploadImage", formData);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}