using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Recipes.Application.Permissions;
using Recipes.Application.Services.Activity;
using Recipes.Domain.Models;
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

        public ActivityController(IActivityService activityService)
        {
            _activityService = activityService;
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
        
        [HttpGet("userActivity")]
        [ProducesResponseType(typeof(UserActivity), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<UserActivity>> GetUserActivity()
        {
            return await _activityService.GetUserActivity(HttpContext.User.GetClaims());
        }
    }
}