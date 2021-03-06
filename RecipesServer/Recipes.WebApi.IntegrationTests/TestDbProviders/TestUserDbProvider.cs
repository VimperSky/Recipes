using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Recipes.Application.Services.User;
using Recipes.Domain.Models;
using Recipes.Infrastructure;
using Xunit;

namespace Recipes.WebApi.IntegrationTests.TestDbProviders
{
    public class TestUserDbProvider : IClassFixture<TestWebFactory<Startup>>
    {
        private static readonly User[] Users =
        {
            CreateUser("TestIgor", "Igor!!$$222", "Игорь", "Я игорь!"),
            CreateUser("TestKolyan", "^>y9[N'7s5r$F*7)", "Николай", "")
        };

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

        private static User CreateUser(string login, string password, string name, string bio)
        {
            var (hash, salt) = HashingTools.QuickHash(password);
            var newUser = new User
            {
                Bio = bio,
                Name = name,
                Login = login,
                PasswordSalt = salt,
                PasswordHash = hash
            };

            return newUser;
        }

        public static void FillDbWithData(RecipesDbContext dbContext)
        {
            foreach (var t in Users) dbContext.Users.Add(t);
        }
    }
}