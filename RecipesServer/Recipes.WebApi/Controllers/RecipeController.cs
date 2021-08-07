using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
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
    public class RecipeController: ControllerBase
    {
        private readonly ILogger<RecipesController> _logger;
        private readonly RecipesService _recipesService;

        public RecipeController(ILogger<RecipesController> logger, RecipesService recipesService)
        {
            _logger = logger;
            _recipesService = recipesService;
        }

        
        /// <summary>
        /// Get detail information about recipe
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="400">Invalid input data</response>
        /// <response code="404">Recipe not found</response>
        [HttpGet("detail")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<RecipeDetailDto> GetRecipeDetail([FromQuery][Required, Range(1, int.MaxValue)]int id)
        {
            var detail = _recipesService.GetRecipeDetail(id);
            if (detail == null)
                return NotFound();
            
            return detail;
        }

        /// <summary>
        /// Create a new recipe
        /// </summary>
        /// <param name="recipeCreateDto"></param>
        /// <returns></returns>
        [HttpPost("create")]
        [Authorize]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(UnauthorizedResult), StatusCodes.Status401Unauthorized)]
        public ActionResult<int> CreateRecipe([FromBody]RecipeCreateDto recipeCreateDto)
        {
            try
            {
                var recipeId = _recipesService.CreateRecipe(recipeCreateDto);
                return CreatedAtAction(nameof(GetRecipeDetail), new { id = recipeId }, recipeId);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
        
        /// <summary>
        /// Edit an existing recipe
        /// </summary>
        /// <param name="recipeEditDto"></param>
        /// <returns></returns>
        [HttpPatch("edit")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(UnauthorizedResult), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public ActionResult EditRecipe([FromBody]RecipeEditDto recipeEditDto)
        {
            _recipesService.EditRecipe(recipeEditDto);
            return NoContent();
        }
        
        
        /// <summary>
        /// Delete an existing recipe
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(UnauthorizedResult), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public ActionResult DeleteRecipe([FromQuery]int id)
        {
            _recipesService.DeleteRecipe(id);
            return NoContent();
        }
        
    }
}