using AutoMapper;
using Recipes.Domain.Models;
using Recipes.WebApi.DTO.Recipe;

namespace Recipes.WebApi.Profiles
{
    public class RecipeProfile: Profile
    {
        public RecipeProfile()
        {
            CreateMap<RecipeIngredientBlock, Ingredient>();
            CreateMap<Recipe, RecipePreview>()
                .ForMember(dest => dest.ImagePath, 
                    opt => opt.MapFrom(x => Utils.GetRecipeImagePath(x.ImagePath)));
            
            CreateMap<Recipe, RecipeDetail>()
                .ForMember(dest => dest.Ingredients,
                    opt => opt.MapFrom(x => x.IngredientBlocks))
                .ForMember(dest => dest.ImagePath, 
                    opt => opt.MapFrom(x => Utils.GetRecipeImagePath(x.ImagePath)));
        }
    }
}