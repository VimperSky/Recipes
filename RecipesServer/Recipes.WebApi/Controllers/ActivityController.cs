using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Recipes.Application.Permissions;
using Recipes.Application.Services.Activity;
using Recipes.Domain.Models;
using Recipes.WebApi.DTOs.Activity;
using Recipes.WebApi.ExceptionHandling;

namespace Recipes.WebApi.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ActivityController : ControllerBase
    {
        private readonly IActivityService _activityService;
        private readonly IMapper _mapper;

        public ActivityController(IActivityService activityService, IMapper mapper)
        {
            _activityService = activityService;
            _mapper = mapper;
        }
        
        [HttpPut("like")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> SetLike([FromQuery, Required, Range(1, int.MaxValue)]int recipeId)
        {
            await _activityService.AddActivity(recipeId, HttpContext.User.GetClaims(), ActivityType.Like);
            return Ok();
        }

        [HttpDelete("like")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> DeleteLike([FromQuery, Required, Range(1, int.MaxValue)]int recipeId)
        {
            await _activityService.RemoveActivity(recipeId, HttpContext.User.GetClaims(), ActivityType.Like);
            return Ok();
        }
        
        [HttpPut("star")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> SetStar([FromQuery, Required, Range(1, int.MaxValue)]int recipeId)
        {
            await _activityService.AddActivity(recipeId, HttpContext.User.GetClaims(), ActivityType.Star);
            return Ok();
        }

        [HttpDelete("star")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteStar([FromQuery, Required, Range(1, int.MaxValue)]int recipeId)
        {
            await _activityService.RemoveActivity(recipeId, HttpContext.User.GetClaims(), ActivityType.Star);
            return Ok();
        }
        
        [HttpPost("myRecipesActivity")]
        [ProducesResponseType(typeof(UserRecipesActivityResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<UserRecipesActivityResultDTO>> GetUserRecipesActivity(
            [FromBody]UserRecipesActivityRequestDTO userRecipesActivityRequestDTO)
        {
            var userRecipesActivity = await _activityService.GetUserRecipesActivity(userRecipesActivityRequestDTO.RecipeIds, HttpContext.User.GetClaims());
            return _mapper.Map<UserRecipesActivityResultDTO>(userRecipesActivity);
        }
    }
}