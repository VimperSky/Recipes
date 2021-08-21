using System.Collections.Generic;
using AutoMapper;
using Recipes.Application;
using Recipes.Application.DTOs.Recipe;
using Recipes.Domain.Models;
using Recipes.Infrastructure;

namespace Recipes.WebApi.IntegrationTests.TestDbProviders
{
    public class TestRecipesDbProvider
    {
        private readonly RecipeDetailDto[] _detailedList;

        public readonly RecipePreviewDto[] List;

        public TestRecipesDbProvider()
        {
            var mapper = ApplicationServicesExtensions.CreateMapper();

            _detailedList = mapper.Map<RecipeDetailDto[]>(StartDataDbInitializer.Recipes);
            List = mapper.Map<RecipePreviewDto[]>(StartDataDbInitializer.Recipes);
        }

        public RecipeDetailDto Detail(int id)
        {
            return _detailedList[id - 1];
        }
    }
}