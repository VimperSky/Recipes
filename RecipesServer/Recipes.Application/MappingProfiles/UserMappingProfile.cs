using AutoMapper;
using Recipes.Application.DTOs.User;
using Recipes.Domain.Models;

namespace Recipes.Application.MappingProfiles
{
    public class UserMappingProfile: Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, UserProfileInfoDto>();
        }
    }
}