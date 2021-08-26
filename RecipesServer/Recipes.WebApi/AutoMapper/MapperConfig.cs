using AutoMapper;
using Recipes.Application.MappingProfiles;

namespace Recipes.WebApi.AutoMapper
{
    public static class MapperConfig
    {
        public static IMapper CreateMapper()
        {
            var mappingConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ApplicationMappingProfile>();
                cfg.AddProfile<WebApiMappingProfile>();
            });
            return mappingConfig.CreateMapper();
        }
    }
}