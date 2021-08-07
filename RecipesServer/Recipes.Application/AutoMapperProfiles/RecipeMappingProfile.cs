using AutoMapper;
using Recipes.Application.DTOs.Recipe;
using Recipes.Domain.Models;

namespace Recipes.Application.AutoMapperProfiles
{
    public class RecipeMappingProfile: Profile
    {
        public RecipeMappingProfile()
        {
            CreateMap<RecipeIngredientBlock, IngredientDto>();
            CreateMap<Recipe, RecipePreviewDto>()
                .ForMember(dest => dest.ImagePath, 
                    opt => opt.MapFrom(x => ApplicationUtils.GetRecipeImagePath(x.ImagePath)));
            
            CreateMap<Recipe, RecipeDetailDto>()
                .ForMember(dest => dest.Ingredients,
                    opt => opt.MapFrom(x => x.IngredientBlocks))
                .ForMember(dest => dest.ImagePath, 
                    opt => opt.MapFrom(x => ApplicationUtils.GetRecipeImagePath(x.ImagePath)));

            CreateMap<RecipeCreateDto, Recipe>();
            CreateMap<RecipeEditDto, Recipe>();
        }
    }
}