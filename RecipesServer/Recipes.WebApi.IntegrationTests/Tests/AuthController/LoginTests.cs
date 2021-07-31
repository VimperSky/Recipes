using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Recipes.WebApi.AuthFeatures.Models;
using Recipes.WebApi.DTO.Auth;
using Recipes.WebApi.Tests.HelpClasses;
using Xunit;

namespace Recipes.WebApi.Tests.Tests.AuthController
{    
    [Collection("Tests")]
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
            var dto = new LoginDto {Login = login, Password = "12345678"};
            
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
            var dto = new LoginDto {Login = TestAuthProvider.GetTestLogin(), Password = password};
            
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
            var dto = new LoginDto {Login = "NonExisting", Password = "abcdefgregh"};

            // Act
            var response = await _client.PostAsJsonAsync($"{BaseAddress}/login", dto);
            var content = JsonConvert.DeserializeObject<ProblemDetails>(await response.Content.ReadAsStringAsync());

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
            Assert.Equal(LoginException.LoginDoesNotExist, content.Detail);            
        }
        
        
        [Fact]
        public async Task Post_Login_IncorrectPassword_ReturnsUnauthorized()
        {
            // Arrange
            // Регистрируем тестовый аккаунт
            var testLogin = TestAuthProvider.GetTestLogin();
            var regDto = new RegisterDto {Login = testLogin, Name = "test", Password = "abcd1234"};
            await _client.PostAsJsonAsync($"{BaseAddress}/register", regDto);
            
            var dto = new LoginDto {Login = testLogin, Password = "abcd12345"};
            
            // Act
            var response = await _client.PostAsJsonAsync($"{BaseAddress}/login", dto);
            var content = JsonConvert.DeserializeObject<ProblemDetails>(await response.Content.ReadAsStringAsync());

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
            Assert.Equal(LoginException.LoginDoesNotExist, content.Detail);            
        }
        #endregion
    }
}