using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Recipes.Application.DTOs.Tags;
using Recipes.Application.Services.Tags;

namespace Recipes.WebApi.Controllers
{    
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class TagsController
    {
        private readonly ITagsService _tagsService;

        public TagsController(ITagsService tagsService)
        {
            _tagsService = tagsService;
        }

        [HttpGet("suggested")]
        [ProducesResponseType(typeof(SuggestedTagsDto), StatusCodes.Status200OK)]
        public async Task<SuggestedTagsDto> GetSuggestedSearchTags()
        {
            return await _tagsService.GetSuggestedSearchTags();
        }
    }
}