using System.Linq;
using AutoMapper;
using Recipes.Application.DTOs.Recipe;
using Recipes.Application.DTOs.Tags;
using Recipes.Application.DTOs.User;
using Recipes.Domain.Models;

namespace Recipes.Application.MappingProfiles
{
    public class MainMappingProfile : Profile
    {
        public MainMappingProfile()
        {
            // Tags
            CreateMap<Tag, string>().ConvertUsing(x => x.Value);
            CreateMap<string, Tag>().ConvertUsing(x => new Tag {Value = x});
            CreateMap<Tag, TagDto>();
            
            // User
            CreateMap<User, ProfileInfo>();
            CreateMap<User, AuthorDto>();
            
            
            // Recipe
            CreateMap<RecipeIngredientsBlock, IngredientDto>();
            CreateMap<Recipe, RecipePreviewDto>()
                .ForMember(dest => dest.LikesCount,
                    opt => opt.MapFrom(x => x.Activities.Count(a => a.IsLiked)))
                .ForMember(dest => dest.StarsCount,
                    opt => opt.MapFrom(x => x.Activities.Count(a => a.IsStarred)));

            CreateMap<Recipe, RecipeDetailDto>()
                .ForMember(dest => dest.LikesCount,
                    opt => opt.MapFrom(x => x.Activities.Count(a => a.IsLiked)))
                .ForMember(dest => dest.StarsCount,
                    opt => opt.MapFrom(x => x.Activities.Count(a => a.IsStarred)));

            CreateMap<Recipe, RecipeCreateDto>();
            CreateMap<Recipe, RecipeEditDto>();

            CreateMap<IngredientDto, RecipeIngredientsBlock>();
            CreateMap<RecipeCreateDto, Recipe>();
            CreateMap<RecipeEditDto, Recipe>();
        }
    }
}