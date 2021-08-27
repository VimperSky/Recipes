using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Recipes.Application.Services.Tags;
using Recipes.WebApi.DTOs.Tags;

namespace Recipes.WebApi.Controllers
{    
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class TagsController
    {
        private readonly ITagsService _tagsService;
        private readonly IMapper _mapper;

        public TagsController(ITagsService tagsService, IMapper mapper)
        {
            _tagsService = tagsService;
            _mapper = mapper;
        }

        [HttpGet("suggested")]
        [ProducesResponseType(typeof(SuggestedTagsResultDTO), StatusCodes.Status200OK)]
        public async Task<SuggestedTagsResultDTO> GetSuggestedSearchTags()
        {
            var suggestedTags = await _tagsService.GetSuggestedSearchTags();
            return _mapper.Map<SuggestedTagsResultDTO>(suggestedTags);
        }
        
        [HttpGet("featured")]
        [ProducesResponseType(typeof(FeaturedTagsResultDTO), StatusCodes.Status200OK)]
        public async Task<FeaturedTagsResultDTO> GetFeaturedTags()
        {
            var featuredTags = await _tagsService.GetFeaturedTags();
            return _mapper.Map<FeaturedTagsResultDTO>(featuredTags);
        }
        
    }
}