using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json;
using Recipes.Application.DTOs.Recipe;
using Xunit;

namespace Recipes.WebApi.IntegrationTests.Tests.RecipeController
{    
    [Collection("TestsCollection")]
    public class EditTests
    {
        private readonly HttpClient _client;
        
        private const string BaseAddress = "api/recipe";
        
        public static readonly RecipeEditDto TestRecipeEditDto = new()
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
        
        
        [Fact]
        public async Task Patch_Edit_NoAuthorization_ReturnsUnauthorized()
        {
            // Act
            var response = await _client.PatchAsJsonAsync($"{BaseAddress}/edit", TestRecipeEditDto);

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
        
        [Fact]
        public async Task Patch_Edit_OwnedRecipe_ReturnsOk()
        {
            _client.SetAuthToken();
            var createdRecipe = await _client.PostAsJsonAsync($"{BaseAddress}/create", CreateTests.TestRecipeCreateDto);
            createdRecipe.StatusCode.Should().Be(HttpStatusCode.Created);
            var recipeId = JsonConvert.DeserializeObject<int>(await createdRecipe.Content.ReadAsStringAsync());

            var copyRecipe = TestRecipeEditDto;
            copyRecipe.Id = recipeId;
            var editResponse = await _client.PatchAsJsonAsync($"{BaseAddress}/edit", TestRecipeEditDto);
            editResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            
            var detail = await _client.GetFromJsonAsync<RecipeDetailDto>($"{BaseAddress}/detail?id={recipeId}");
            
            detail.Should().BeEquivalentTo(TestRecipeEditDto);
        }
        
        [Fact]
        public async Task Patch_Edit_ForeignRecipe_ReturnsForbidden()
        {
            _client.SetAuthToken();
            var createdRecipe = await _client.PostAsJsonAsync($"{BaseAddress}/create", CreateTests.TestRecipeCreateDto);
            createdRecipe.StatusCode.Should().Be(HttpStatusCode.Created);
            var recipeId = JsonConvert.DeserializeObject<int>(await createdRecipe.Content.ReadAsStringAsync());

            _client.SetAuthToken(true);
            var copyRecipe = TestRecipeEditDto;
            copyRecipe.Id = recipeId;
            var editResponse = await _client.PatchAsJsonAsync($"{BaseAddress}/edit", TestRecipeEditDto);
            editResponse.StatusCode.Should().Be(HttpStatusCode.Forbidden);
            
            var detail = await _client.GetFromJsonAsync<RecipeDetailDto>($"{BaseAddress}/detail?id={recipeId}");
            
            detail.Should().BeEquivalentTo(CreateTests.TestRecipeCreateDto);
        }
    }
}