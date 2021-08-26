using AutoMapper;
using Recipes.Domain.Models;
using Recipes.WebApi.DTOs.Recipe;
using Recipes.WebApi.DTOs.User;

namespace Recipes.WebApi.IntegrationTests.AutoMapper
{
    public class TestsMappingProfile: Profile
    {
        public TestsMappingProfile()
        {
            CreateMap<RecipeIngredientsBlock, IngredientDto>();
            CreateMap<User, AuthorDto>();

            CreateMap<Recipe, RecipePreviewResultDTO>();
            CreateMap<Recipe, RecipeDetailResultResultDTO>();
        }
    }
}