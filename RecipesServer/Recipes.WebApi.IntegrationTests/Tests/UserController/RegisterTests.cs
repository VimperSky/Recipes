using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Recipes.Application.Exceptions;
using Recipes.WebApi.DTO.User;
using Xunit;
using static Recipes.WebApi.IntegrationTests.Tests.UsersController.DataProviders.UserDataProvider;

namespace Recipes.WebApi.IntegrationTests.Tests.UsersController
{
    [Collection("TestsCollection")]
    public class RegisterTests
    {
        private const string BaseAddress = "api/user";
        private readonly HttpClient _client;

        public RegisterTests(TestWebFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        #region Internal Logic Tests

        [Fact]
        public async Task Post_Register_TakenLogin_ReturnsConflict()
        {
            // Arrange
            var login = GetValidLogin();
            var dto = new RegisterDto { Login = login, Name = GetValidName(), Password = GetValidPassword() };

            await _client.PostAsJsonAsync($"{BaseAddress}/register", dto);

            // Act
            var response = await _client.PostAsJsonAsync($"{BaseAddress}/register", dto);
            var content = JsonConvert.DeserializeObject<ProblemDetails>(await response.Content.ReadAsStringAsync());

            // Assert
            Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
            Assert.Equal(UserModificationException.LoginIsTaken, content.Detail);
        }

        #endregion

        #region Invalid Input Data Tests

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("           ")]
        [InlineData("AB")]
        [InlineData("A   B")]
        [InlineData("aaaaaaaaaaaaaaaaaaaaa")]
        public async Task Post_Register_InvalidLogin_ReturnsBadRequest(string login)
        {
            // Arrange
            var dto = new RegisterDto { Login = login, Name = GetValidName(), Password = GetValidPassword() };

            // Act
            var response = await _client.PostAsJsonAsync($"{BaseAddress}/register", dto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Theory]
        [InlineData("abc")]
        [InlineData("aaaaaaaaaaaaaaaaaaaa")]
        public async Task Post_Register_ValidLogin_ReturnsOk(string login)
        {
            // Arrange
            var dto = new RegisterDto { Login = login, Name = GetValidName(), Password = GetValidPassword() };

            // Act
            var response = await _client.PostAsJsonAsync($"{BaseAddress}/register", dto);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("           ")]
        [InlineData("AB")]
        [InlineData("A   B")]
        [InlineData("aaaaaaaaaaaaaaaaaaaaaaaaa")]
        public async Task Post_Register_InvalidPassword_ReturnsBadRequest(string password)
        {
            // Arrange
            var dto = new RegisterDto { Login = GetValidLogin(), Name = GetValidName(), Password = password };

            // Act
            var response = await _client.PostAsJsonAsync($"{BaseAddress}/register", dto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Theory]
        [InlineData("abcd1234")]
        [InlineData("aaaaaaaaaaaaaaaaaaaaaaaa")]
        public async Task Post_Register_ValidPassword_ReturnsOK(string password)
        {
            // Arrange
            var dto = new RegisterDto { Login = GetValidLogin(), Name = GetValidName(), Password = password };

            // Act
            var response = await _client.PostAsJsonAsync($"{BaseAddress}/register", dto);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("           ")]
        [InlineData("aaaaaaaaaaaaaaaaaaaaa")]
        public async Task Post_Register_InvalidName_ReturnsBadRequest(string name)
        {
            // Arrange
            var dto = new RegisterDto { Login = GetValidLogin(), Name = name, Password = GetValidPassword() };

            // Act
            var response = await _client.PostAsJsonAsync($"{BaseAddress}/register", dto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Theory]
        [InlineData("aaaaaaaaaaaaaaaaaaaa")]
        public async Task Post_Register_ValidName_ReturnsOk(string name)
        {
            // Arrange
            var dto = new RegisterDto { Login = GetValidLogin(), Name = name, Password = GetValidPassword() };

            // Act
            var response = await _client.PostAsJsonAsync($"{BaseAddress}/register", dto);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        #endregion
    }
}