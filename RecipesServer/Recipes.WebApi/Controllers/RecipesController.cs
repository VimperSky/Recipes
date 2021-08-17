using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Recipes.Application.DTOs.Recipe;
using Recipes.Application.Permissions;
using Recipes.Application.Services.Recipes;
using Recipes.WebApi.ExceptionHandling;

namespace Recipes.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class RecipesController : ControllerBase
    {
        private readonly IRecipesService _recipesService;

        public RecipesController(IRecipesService recipesService)
        {
            _recipesService = recipesService;
        }

        /// <summary>
        ///     Retrieve a list of recipes
        /// </summary>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="400">Invalid input data</response>
        /// <response code="404">Page with this id doesn't exist</response>
        [HttpGet("list")]
        [ProducesResponseType(typeof(RecipesPageDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<RecipesPageDto>> GetRecipes(
            [FromQuery] [Required] [Range(1, int.MaxValue)] int pageSize,
            [FromQuery] [Range(1, int.MaxValue)] int page = 1, [FromQuery] string searchString = null)
        {
            return await _recipesService.GetRecipesPage(pageSize, page, searchString);
        }
        
        
        [HttpGet("myList")]
        [Authorize]
        [ProducesResponseType(typeof(RecipesPageDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<RecipesPageDto>> GetMyRecipes(
            [FromQuery] [Required] [Range(1, int.MaxValue)] int pageSize, 
            [FromQuery] [Range(1, int.MaxValue)] int page = 1)
        {
            return await _recipesService.GetRecipesPage(pageSize, page, authorClaims: HttpContext.User.GetClaims());
        }
    }
}