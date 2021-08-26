using Recipes.Infrastructure;
using Recipes.WebApi.AutoMapper;
using Recipes.WebApi.DTOs.Recipe;
using Recipes.WebApi.IntegrationTests.AutoMapper;

namespace Recipes.WebApi.IntegrationTests.TestDbProviders
{
    public class TestRecipesDbProvider
    {
        private readonly RecipeDetailResultResultDTO[] _detailedList;

        public readonly RecipePreviewResultDTO[] List;

        public TestRecipesDbProvider()
        {
            var mapper = TestsMappingConfig.CreateTestMapper();

            _detailedList = mapper.Map<RecipeDetailResultResultDTO[]>(StartDataDbInitializer.Recipes);
            List = mapper.Map<RecipePreviewResultDTO[]>(StartDataDbInitializer.Recipes);
        }

        public RecipeDetailResultResultDTO Detail(int id)
        {
            return _detailedList[id - 1];
        }
    }
}