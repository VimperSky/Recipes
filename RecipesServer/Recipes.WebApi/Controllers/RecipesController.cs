using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Recipes.Application.DTOs.Recipe;
using Recipes.Application.Services.Recipes;

namespace Recipes.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class RecipesController : ControllerBase
    {
        private readonly ILogger<RecipesController> _logger;
        private readonly IRecipesService _recipesService;

        public RecipesController(ILogger<RecipesController> logger, IRecipesService recipesService)
        {
            _logger = logger;
            _recipesService = recipesService;
        }
        
        /// <summary>
        /// Retrieve a list of recipes
        /// </summary>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="400">Invalid input data</response>
        /// <response code="404">Page with this id doesn't exist</response>
        [HttpGet("list")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RecipesPageDto>> GetRecipes([FromQuery][Required, Range(1, int.MaxValue)]int pageSize, 
            [FromQuery][Range(1, int.MaxValue)]int page = 1, [FromQuery]string searchString = "")
        {
            try
            {
                var recipesPage = await _recipesService.GetRecipesPage(searchString, pageSize, page);
                return recipesPage;
            }
            catch (ArgumentOutOfRangeException e)
            {
                return Problem($"Parameter is not correct: {e.ParamName}", statusCode: 400);
            }
            catch (Exception e)
            {
                _logger.LogError("Internal server error happened while processing client request with parameters:\r\n" +
                                 $"pageSize: {pageSize}, page: {page}, searchString: {searchString}. Error text:\r\n" + e);
                return Problem("Unknown error happened while processing your request.", statusCode: 400);
            }
        }
    }
}