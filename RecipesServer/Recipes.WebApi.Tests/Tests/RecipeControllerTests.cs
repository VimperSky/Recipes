using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Newtonsoft.Json;
using Recipes.Domain.Models;
using Recipes.WebApi.DTO.Recipe;
using Recipes.WebApi.Profiles;
using Recipes.WebApi.Tests.HelpClasses;
using Xunit;

namespace Recipes.WebApi.Tests.Tests
{
    [Collection("Tests")]
    public class RecipeControllerTest: IClassFixture<TestWebFactory<Startup>>
    {
        private readonly IMapper _mapper;
        private readonly HttpClient _client;

        private const string BaseAddress = "api/recipe";
        
        public RecipeControllerTest(TestWebFactory<Startup> factory)
        {
            _client = factory.CreateClient();
            
            var configuration = new MapperConfiguration(cfg =>
                cfg.AddMaps(typeof(Startup)));
            _mapper = configuration.CreateMapper();
        }
        
        [Theory]
        [InlineData("-1")]
        [InlineData("test")]
        public async Task Get_Detail_InvalidRecipeId_ReturnsBadRequest(string id)
        {
            // Act
            var response = await _client.GetAsync($"{BaseAddress}/detail?id={id}");
            
            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
        
        [Fact]
        public async Task Get_Detail_NonExistingRecipeId_ReturnsNotFound()
        {
            // Act
            var response = await _client.GetAsync($"{BaseAddress}/detail?id=555");
            
            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
        
        [Fact]
        public async Task Get_Detail_ExistingRecipeId_ReturnsValue()
        {
            // Arrange
            const int recipeId = 1;

            // Act
            var response = await _client.GetAsync($"{BaseAddress}/detail?id={recipeId}");
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<RecipeDetailDto>(content);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            CustomAssert.Equal(_mapper.Map<RecipeDetailDto>(TestDbCreator.AllRecipes[recipeId - 1]), result);
        }
        
        [Fact]
        public async Task Get_Detail_RecipeIdNotPassed_ReturnsBadRequest()
        {
            // Act
            var response = await _client.GetAsync($"{BaseAddress}/detail");

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}