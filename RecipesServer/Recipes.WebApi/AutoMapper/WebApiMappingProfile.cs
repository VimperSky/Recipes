using AutoMapper;
using Recipes.Application.Models.Recipe;
using Recipes.Application.Models.Tags;
using Recipes.Application.Models.User;
using Recipes.Domain.Models;
using Recipes.WebApi.DTOs.Activity;
using Recipes.WebApi.DTOs.Recipe;
using Recipes.WebApi.DTOs.Tags;
using Recipes.WebApi.DTOs.User;

namespace Recipes.WebApi.AutoMapper
{
    public class WebApiMappingProfile : Profile
    {
        public WebApiMappingProfile()
        {
            // Result -> ResultDTO
            CreateMap<TagInfo, TagDto>();
            CreateMap<AuthorInfo, AuthorDto>();
            CreateMap<Ingredient, IngredientDto>();

            CreateMap<UserRecipesActivityResult, UserRecipesActivityResultDTO>();
            CreateMap<RecipeDetailResult, RecipeDetailResultDTO>();
            CreateMap<RecipePreviewResult, RecipePreviewResultDTO>();
            CreateMap<SuggestedTagsResult, SuggestedTagsResultDTO>();
            CreateMap<FeaturedTagsResult, FeaturedTagsResultDTO>();
            CreateMap<ProfileInfoResult, ProfileInfoResultDTO>();
            CreateMap<RecipesPageResult, RecipesPageResultDTO>();
            
            // RequestDTO -> Command
            CreateMap<IngredientDto, Ingredient>();

            CreateMap<RecipeCreateRequestDTO, RecipeCreateCommand>();
            CreateMap<RecipeEditRequestDTO, RecipeEditCommand>();
        }
    }
}