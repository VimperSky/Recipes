using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Recipes.Application.DTOs.Recipe;
using Recipes.WebApi.Tests.Utils;
using Xunit;

namespace Recipes.WebApi.Tests.Tests.RecipeController
{    
    [Collection("Tests")]
    public class EditTests: IClassFixture<TestWebFactory<Startup>>
    {       
        private readonly HttpClient _client;
        
        private const string BaseAddress = "api/recipe";
        
        private static readonly RecipeEditDto TestRecipeEditDto = new()
        {
            Name = "Какое-то название",
            Description = "Описание рецепта",
            Ingredients = new [] 
            {
                new IngredientDto 
                { 
                    Header = "заголовок 1", Value = "Клубника\nМолоко"
                },
                new IngredientDto
                {
                    Header = "Заголовок 2", Value = "Какао\nЧто-тоеще"
                }
            },
            Steps = new []
            {
                "Берем что-то там",
                "Делаем что-то с этим",
                "Еще что-то делаем",
                "Готово!"
            },
            CookingTimeMin = 60,
            Portions = 5
        };
        
        public EditTests(TestWebFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }
        
        // [Fact]
        // public async Task Post_Edit_NoAuthorization_ReturnsUnauthorized()
        // {
        //     // Arrange
        //     var dto = TestRecipeEditDto;
        //     dto.Id = 999;
        //     
        //     // Act
        //     var response = await _client.PostAsJsonAsync($"{BaseAddress}/create", TestRecipeEditDto);
        //
        //     // Assert
        //     Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        // }
        //
    }
}