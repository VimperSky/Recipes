using AutoMapper;
using Recipes.Domain.Models;
using Recipes.WebApi.DTO.Recipe;

namespace Recipes.WebApi.Profiles
{
    public class RecipeProfile: Profile
    {
        public RecipeProfile()
        {
            CreateMap<RecipeIngredientBlock, IngredientDto>();
            CreateMap<Recipe, RecipePreviewDto>()
                .ForMember(dest => dest.ImagePath, 
                    opt => opt.MapFrom(x => Utils.GetRecipeImagePath(x.ImagePath)));
            
            CreateMap<Recipe, RecipeDetailDto>()
                .ForMember(dest => dest.Ingredients,
                    opt => opt.MapFrom(x => x.IngredientBlocks))
                .ForMember(dest => dest.ImagePath, 
                    opt => opt.MapFrom(x => Utils.GetRecipeImagePath(x.ImagePath)));
        }
    }
}