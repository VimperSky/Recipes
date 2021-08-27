using AutoMapper;

namespace Recipes.Application.MappingProfiles
{
    public static class ApplicationMappingConfig
    {
        public static IMapper CreateApplicationMapper()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ApplicationMappingProfile>();
            });
            return configuration.CreateMapper();
        }
    }
}