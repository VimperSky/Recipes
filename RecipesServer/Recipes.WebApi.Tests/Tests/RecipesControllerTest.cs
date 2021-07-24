using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using Newtonsoft.Json;
using Recipes.WebApi.DTO.Recipe;
using Recipes.WebApi.Tests.HelpClasses;
using Xunit;

namespace Recipes.WebApi.Tests.Tests
{
    [Collection("Tests")]
    public class RecipesControllerTest: IClassFixture<TestWebFactory<Startup>>
    {
        private readonly HttpClient _client;
        private const string BaseAddress = "api/recipes";
        private readonly IMapper _mapper;

        public RecipesControllerTest(TestWebFactory<Startup> factory)
        {
            _client = factory.CreateClient();
            
            var configuration = new MapperConfiguration(cfg =>
                cfg.AddMaps(typeof(Startup)));
            _mapper = configuration.CreateMapper();
        }
        
        
        [Fact]
        public async Task Get_RecipeList_NoPageSizePassed_ReturnsBadRequest()
        {
            // Act
            var response = await _client.GetAsync($"{BaseAddress}/list");
            
            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
        
        [Theory]
        [InlineData("test")]
        [InlineData("-5")]
        [InlineData("0")]
        public async Task Get_RecipeList_InvalidPage_ReturnsBadRequest(string page)
        {
            // Arrange
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["pageSize"] = Constants.PageSize.ToString();
            query["page"] = page;
            
            // Act
            var response = await _client.GetAsync($"{BaseAddress}/list?{query}");

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
        
        
        [Fact]
        public async Task Get_RecipeList_NonExistingPage_ReturnsNotFound()
        {
            // Arrange
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["pageSize"] = Constants.PageSize.ToString();
            query["page"] = "5";
            
            // Act
            var response = await _client.GetAsync($"{BaseAddress}/list?{query}");
            
            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
        
        [Fact]
        public async Task Get_RecipeList_NoPagePassed_ReturnsFirstPage()
        {
            // Arrange
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["pageSize"] = Constants.PageSize.ToString();
            
            // Act
            var response = await _client.GetAsync($"{BaseAddress}/list?{query}");
            var result = JsonConvert.DeserializeObject<RecipesPage>(await response.Content.ReadAsStringAsync());

            
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            CustomAssert.Equal(_mapper.Map<RecipePreview[]>(TestDbCreator.FirstPageRecipes), result.Recipes);
        }

        
        [Fact]
        public async Task Get_RecipeList_PassedExistingPage_ReturnThisPage()
        {
            // Arrange
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["pageSize"] = Constants.PageSize.ToString();
            query["page"] = "1";
            
            // Act
            var response = await _client.GetAsync($"{BaseAddress}/list?{query}");
            var result = JsonConvert.DeserializeObject<RecipesPage>(await response.Content.ReadAsStringAsync());

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            CustomAssert.Equal(_mapper.Map<RecipePreview[]>(TestDbCreator.FirstPageRecipes), result.Recipes);
        }
        
        
        [Fact]
        public async Task Get_RecipeList_SearchForNonExistingItems_ReturnEmptyRecipeList()
        {
            // Arrange
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["pageSize"] = Constants.PageSize.ToString();
            query["searchString"] = "abcdef";

            // Act
            var response = await _client.GetAsync($"{BaseAddress}/list?{query}");
            var result = JsonConvert.DeserializeObject<RecipesPage>(await response.Content.ReadAsStringAsync());

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Empty(result.Recipes);
        }
        
        [Fact]
        public async Task Get_RecipeList_SearchForExistingItems_ReturnExpectedRecipeList()
        {
            // Arrange
            const string searchString = "па";
            
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["pageSize"] = Constants.PageSize.ToString();
            query["searchString"] = searchString;
            
            // Act
            var response = await _client.GetAsync($"{BaseAddress}/list?{query}");
            var result = JsonConvert.DeserializeObject<RecipesPage>(await response.Content.ReadAsStringAsync());

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            CustomAssert.Equal(_mapper.Map<RecipePreview[]>(TestDbCreator.AllRecipes.Where(x => x.Name.ToLower().Contains(searchString))), result.Recipes);
        }
    }
}