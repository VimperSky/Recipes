using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Recipes.Application.Exceptions;
using Recipes.WebApi.DTO.Auth;
using Xunit;
using static Recipes.WebApi.IntegrationTests.Tests.AuthController.UserDataProvider;

namespace Recipes.WebApi.IntegrationTests.Tests.AuthController
{    
    public class LoginTests: IClassFixture<TestWebFactory<Startup>>
    {
        private readonly HttpClient _client;

        private const string BaseAddress = "api/auth";
        
        public LoginTests(TestWebFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }
        
        #region Invalid Input Data Tests
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("           ")]
        [InlineData("AB")]
        [InlineData("A   B" )]
        [InlineData("aaaaaaaaaaaaaaaaaaaaa")]
        public async Task Post_Login_InvalidLogin_ReturnsBadRequest(string login)
        {
            // Arrange
            var dto = new LoginDto {Login = login, Password = GetValidPassword()};
            
            // Act
            var response = await _client.PostAsJsonAsync($"{BaseAddress}/login", dto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
        
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("           ")]
        [InlineData("AB")]
        [InlineData("A   B" )]
        [InlineData("aaaaaaaaaaaaaaaaaaaaaaaaa")]
        public async Task Post_Login_InvalidPassword_ReturnsBadRequest(string password)
        {
            // Arrange
            var dto = new LoginDto {Login = GetValidLogin(), Password = password};
            
            // Act
            var response = await _client.PostAsJsonAsync($"{BaseAddress}/login", dto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
        #endregion

        #region Internal Logic Tests
        [Fact]
        public async Task Post_Login_NonExistingAccount_ReturnsUnauthorized()
        {
            // Arrange
            var dto = new LoginDto {Login = "NonExisting", Password = GetValidPassword()};

            // Act
            var response = await _client.PostAsJsonAsync($"{BaseAddress}/login", dto);
            var content = JsonConvert.DeserializeObject<ProblemDetails>(await response.Content.ReadAsStringAsync());

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
            Assert.Equal(UserLoginException.LoginDoesNotExist, content.Detail);            
        }
        
        
        [Fact]
        public async Task Post_Login_IncorrectPassword_ReturnsUnauthorized()
        {
            // Arrange
            // Регистрируем тестовый аккаунт
            var login = GetValidLogin();
            var regDto = new RegisterDto {Login = login, Name = GetValidName(), Password = GetValidPassword()};
            (await _client.PostAsJsonAsync($"{BaseAddress}/register", regDto))
                .StatusCode.Should().Be(HttpStatusCode.OK);
            
            var dto = new LoginDto {Login = login, Password = "nonvalidpassword"};
            
            // Act
            var response = await _client.PostAsJsonAsync($"{BaseAddress}/login", dto);
            var content = JsonConvert.DeserializeObject<ProblemDetails>(await response.Content.ReadAsStringAsync());

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
            Assert.Equal(UserLoginException.PasswordIsIncorrect, content.Detail);            
        }
        
        [Fact]
        public async Task Post_Login_CorrectPassword_ReturnsOk()
        {
            // Arrange
            // Регистрируем тестовый аккаунт
            var login = GetValidLogin();
            var password = GetValidPassword();
            var regDto = new RegisterDto {Login = login, Name = GetValidName(), Password = password};
            (await _client.PostAsJsonAsync($"{BaseAddress}/register", regDto))
                .StatusCode.Should().Be(HttpStatusCode.OK);
            
            var dto = new LoginDto {Login = login, Password = password};
            
            // Act
            var response = await _client.PostAsJsonAsync($"{BaseAddress}/login", dto);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        #endregion
    }
}