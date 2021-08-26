using Recipes.Infrastructure;
using Recipes.WebApi.AutoMapper;
using Recipes.WebApi.DTOs.Recipe;

namespace Recipes.WebApi.IntegrationTests.TestDbProviders
{
    public class TestRecipesDbProvider
    {
        private readonly RecipeDetailResultResultDTO[] _detailedList;

        public readonly RecipePreviewResultDTO[] List;

        public TestRecipesDbProvider()
        {
            var mapper = MapperConfig.CreateMapper();

            _detailedList = mapper.Map<RecipeDetailResultResultDTO[]>(StartDataDbInitializer.Recipes);
            List = mapper.Map<RecipePreviewResultDTO[]>(StartDataDbInitializer.Recipes);
        }

        public RecipeDetailResultResultDTO Detail(int id)
        {
            return _detailedList[id - 1];
        }
    }
}