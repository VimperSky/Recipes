using AutoMapper;
using Recipes.Application.DTOs.Recipe;
using Recipes.Domain.Models;

namespace Recipes.Application.MappingProfiles
{
    public class RecipeMappingProfile: Profile
    {
        public RecipeMappingProfile()
        {
            CreateMap<RecipeIngredientsBlock, IngredientDto>();
            CreateMap<Recipe, RecipePreviewDto>();
            CreateMap<Recipe, RecipeDetailDto>();
            
            CreateMap<IngredientDto, RecipeIngredientsBlock>();
            CreateMap<RecipeCreateDto, Recipe>();
            CreateMap<RecipeEditDto, Recipe>();
        }
    }
}