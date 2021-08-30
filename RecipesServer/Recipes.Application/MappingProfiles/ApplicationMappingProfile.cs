using System.Linq;
using AutoMapper;
using Recipes.Application.Models.Recipe;
using Recipes.Application.Models.Tags;
using Recipes.Application.Models.User;
using Recipes.Domain.Models;

namespace Recipes.Application.MappingProfiles
{
    public class ApplicationMappingProfile : Profile
    {
        public ApplicationMappingProfile()
        {
            // Tags
            CreateMap<Tag, string>().ConvertUsing(x => x.Value);
            CreateMap<string, Tag>().ConvertUsing(x => new Tag { Value = x });

            CreateMap<Tag, TagInfo>();

            // User
            CreateMap<User, ProfileInfoResult>();
            CreateMap<User, AuthorInfo>();


            // Recipe
            CreateMap<Ingredient, RecipeIngredientsBlock>();
            CreateMap<Recipe, RecipePreviewResult>()
                .ForMember(dest => dest.LikesCount,
                    opt => opt.MapFrom(x => x.Activities.Count(a => a.IsLiked)))
                .ForMember(dest => dest.StarsCount,
                    opt => opt.MapFrom(x => x.Activities.Count(a => a.IsStarred)));

            CreateMap<Recipe, RecipeDetailResult>()
                .ForMember(dest => dest.LikesCount,
                    opt => opt.MapFrom(x => x.Activities.Count(a => a.IsLiked)))
                .ForMember(dest => dest.StarsCount,
                    opt => opt.MapFrom(x => x.Activities.Count(a => a.IsStarred)));
            CreateMap<Recipe, RecipeCreateCommand>();
            CreateMap<Recipe, RecipeEditCommand>();

            CreateMap<RecipeIngredientsBlock, Ingredient>();
            CreateMap<RecipeCreateCommand, Recipe>();
            CreateMap<RecipeEditCommand, Recipe>();
        }
    }
}