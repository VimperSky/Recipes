using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Recipes.WebApi.DTO.Auth;
using Recipes.WebApi.Tests.Tests.AuthController;

namespace Recipes.WebApi.Tests.Utils
{
    public static class TestAccountProvider
    {
        private static readonly HttpClient Client = new();
        public static string Token { get; private set; }

        static TestAccountProvider()
        {
            CreateTestAccount().GetAwaiter().GetResult();
        }
        
        private static async Task CreateTestAccount()
        {
            var dto = new RegisterDto 
            {
                Login = UserDataProvider.GetValidLogin(), 
                Name = UserDataProvider.GetValidName(),
                Password = UserDataProvider.GetValidPassword()
            };
            
            var resp = await Client.PostAsJsonAsync("api/auth/register", dto);
            resp.StatusCode.Should().Be(HttpStatusCode.OK);


            Token = (await resp.Content.ReadAsStringAsync()).Trim('"');
        }
    }
}