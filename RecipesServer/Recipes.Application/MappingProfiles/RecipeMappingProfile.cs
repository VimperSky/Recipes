using System;
using AutoMapper;
using Recipes.Application.DTOs.Recipe;
using Recipes.Domain.Models;

namespace Recipes.Application.MappingProfiles
{
    public class RecipeMappingProfile: Profile
    {
        public RecipeMappingProfile()
        {
            CreateMap<RecipeIngredientBlock, IngredientDto>();
            CreateMap<Recipe, RecipePreviewDto>();
            
            CreateMap<Recipe, RecipeDetailDto>()
                .ForMember(dest => dest.Ingredients,
                    opt => opt.MapFrom(x => x.IngredientBlocks));

            CreateMap<RecipeCreateDto, Recipe>();
            CreateMap<RecipeEditDto, Recipe>();
        }
    }
}