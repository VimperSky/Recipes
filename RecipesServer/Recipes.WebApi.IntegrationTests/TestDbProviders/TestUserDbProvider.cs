using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Recipes.Application.Services.Auth;
using Recipes.Domain.Models;
using Recipes.Infrastructure;
using Recipes.WebApi.DTO.Auth;
using Xunit;

namespace Recipes.WebApi.IntegrationTests.TestDbProviders
{
    public class TestUserDbProvider: IClassFixture<TestWebFactory<Startup>>
    {
        public static string Token1 { get; private set; }
        public static string Token2 { get; private set; }
        
        public static void SetUserTokens(IConfigurationSection configurationSection)
        {
            var jwtSettings = new JwtSettings();
            configurationSection.Bind(jwtSettings);
            
            var jwtHandler = new JwtHandler(Options.Create(jwtSettings));

            Token1 = GetUserToken(jwtHandler, Users[0]);
            Token2 = GetUserToken(jwtHandler, Users[1]);
        }

        private static string GetUserToken(JwtHandler jwtHandler, User user)
        {
            var tokenOptions = jwtHandler.GenerateTokenOptions(user);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return token;
        }

        private static User CreateUser(int id, string login, string password, string name, string bio)
        {
            var salt = HashingTools.GenerateSalt();
            var hash = HashingTools.HashPassword(password, salt);
            var newUser = new User
            {
                Id  = id,
                Bio = bio,
                Name = name,
                Login = login,
                PasswordSalt = HashingTools.SaltToString(salt),
                PasswordHash = hash
            };
            
            return newUser;
        }

        private static readonly User[] Users =
        {
            CreateUser(1, "TestIgor", "Igor!!$$222", "Игорь", "Я игорь!"),
            CreateUser(2, "TestKolyan", "^>y9[N'7s5r$F*7)", "Николай", "")
        };

        public static void FillDbWithData(RecipesDbContext dbContext)
        {
            foreach (var user in Users)
                dbContext.Users.Add(user);
        }
    }
}