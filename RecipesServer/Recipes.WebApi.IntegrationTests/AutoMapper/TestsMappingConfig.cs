using AutoMapper;
using Recipes.Application.MappingProfiles;
using Recipes.WebApi.AutoMapper;

namespace Recipes.WebApi.IntegrationTests.AutoMapper
{
    internal static class TestsMappingConfig
    {
        internal static IMapper CreateTestMapper()
        {
            var mappingConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ApplicationMappingProfile>();
                cfg.AddProfile<WebApiMappingProfile>();
                cfg.AddProfile<TestsMappingProfile>();
            });
            return mappingConfig.CreateMapper();
        }
    }
}