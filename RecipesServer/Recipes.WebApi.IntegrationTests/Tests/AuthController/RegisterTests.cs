using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Recipes.WebApi.AuthFeatures.Models;
using Recipes.WebApi.DTO.Auth;
using Xunit;

using static Recipes.WebApi.Tests.Tests.AuthController.UserDataProvider;

namespace Recipes.WebApi.Tests.Tests.AuthController
{    
    [Collection("Tests")]
    public class RegisterTests: IClassFixture<TestWebFactory<Startup>>
    {
        private readonly HttpClient _client;

        private const string BaseAddress = "api/auth";
        
        public RegisterTests(TestWebFactory<Startup> factory)
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
        public async Task Post_Register_InvalidLogin_ReturnsBadRequest(string login)
        {
            // Arrange
            var dto = new RegisterDto {Login = login, Name = GetValidName(), Password = GetValidPassword()};
            
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
            var dto = new RegisterDto {Login = login, Name = GetValidName(), Password = GetValidPassword()};
            
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
        [InlineData("A   B" )]
        [InlineData("aaaaaaaaaaaaaaaaaaaaaaaaa")]
        public async Task Post_Register_InvalidPassword_ReturnsBadRequest(string password)
        {
            // Arrange
            var dto = new RegisterDto {Login = GetValidLogin(), Name = GetValidName(), Password = password};
            
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
            var dto = new RegisterDto {Login = GetValidLogin(), Name = GetValidName(), Password = password};
            
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
            var dto = new RegisterDto {Login = GetValidLogin(), Name = name, Password = GetValidPassword()};
            
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
            var dto = new RegisterDto {Login = GetValidLogin(), Name = name, Password = GetValidPassword()};
            
            // Act
            var response = await _client.PostAsJsonAsync($"{BaseAddress}/register", dto);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        #endregion

        #region Internal Logic Tests
        [Fact]
        public async Task Post_Register_TakenLogin_ReturnsConflict()
        {
            // Arrange
            var login = GetValidLogin();
            var dto = new RegisterDto {Login = login, Name = GetValidName(), Password = GetValidPassword()};
            
            await _client.PostAsJsonAsync($"{BaseAddress}/register", dto);

            // Act
            var response = await _client.PostAsJsonAsync($"{BaseAddress}/register", dto);
            var content = JsonConvert.DeserializeObject<ProblemDetails>(await response.Content.ReadAsStringAsync());

            // Assert
            Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
            Assert.Equal(UserRegistrationException.LoginIsTaken, content.Detail);
        }
        #endregion

    }
}