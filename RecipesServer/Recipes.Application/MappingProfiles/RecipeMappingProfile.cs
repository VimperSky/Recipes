using AutoMapper;
using Recipes.Application.DTOs.Recipe;
using Recipes.Domain.Models;

namespace Recipes.Application.MappingProfiles
{
    public class RecipeMappingProfile : Profile
    {
        public RecipeMappingProfile()
        {
            CreateMap<Tag, string>().ConvertUsing(x => x.Value);
            CreateMap<string, Tag>().ConvertUsing(x => new Tag {Value = x});
            
            CreateMap<RecipeIngredientsBlock, IngredientDto>();
            CreateMap<Recipe, RecipePreviewDto>();
            CreateMap<Recipe, RecipeDetailDto>();
            
            CreateMap<Recipe, RecipeCreateDto>();
            CreateMap<Recipe, RecipeEditDto>();

            CreateMap<IngredientDto, RecipeIngredientsBlock>();
            CreateMap<RecipeCreateDto, Recipe>();
            CreateMap<RecipeEditDto, Recipe>();
        }
    }
}