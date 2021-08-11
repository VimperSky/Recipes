using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Recipes.Application.DTOs.Recipe;
using Recipes.Application.Exceptions;
using Recipes.Application.Permissions;
using Recipes.Application.Services.Recipes;
using Recipes.WebApi.DTO.Recipe;

namespace Recipes.WebApi.Controllers
{   
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class RecipeController: ControllerBase
    {
        private readonly ILogger<RecipesController> _logger;
        private readonly IRecipesService _recipesService;

        public RecipeController(ILogger<RecipesController> logger, IRecipesService recipesService)
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
        public async Task<ActionResult<RecipeDetailDto>> GetRecipeDetail([FromQuery][Required, Range(1, int.MaxValue)]int id)
        {
            var detail = await _recipesService.GetRecipeDetail(id);
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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(UnauthorizedResult), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<int>> CreateRecipe([FromBody]RecipeCreateDto recipeCreateDto)
        {
            try
            {
                var recipeId = await _recipesService.CreateRecipe(recipeCreateDto, HttpContext.User.GetPermissions());
                return CreatedAtAction(nameof(GetRecipeDetail), new { id = recipeId }, recipeId);
            }
            catch (PermissionException ex)
            {
                return Problem(ex.Value, statusCode:403);
            }
            catch (Exception ex)
            {
                _logger.LogError("An exception happened while processing CreateRecipe\r\n" + ex);
                return Problem("Unknown error happened while processing your request.", statusCode: 400);
            }
        }
        
        /// <summary>
        /// Edit an existing recipe
        /// </summary>
        /// <param name="recipeEditDto"></param>
        /// <returns></returns>
        [HttpPatch("edit")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(UnauthorizedResult), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> EditRecipe([FromBody]RecipeEditDto recipeEditDto)
        {
            try
            {
                await _recipesService.EditRecipe(recipeEditDto, HttpContext.User.GetPermissions());
                return Ok();
            }
            catch (PermissionException ex)
            {
                return Problem(ex.Value, statusCode:403);
            }
            catch (Exception ex)
            {
                _logger.LogError("An exception happened while processing EditRecipe\r\n" + ex);
                return Problem("Unknown error happened while processing your request.", statusCode: 400);
            }
        }
        
        
        /// <summary>
        /// Delete an existing recipe
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(UnauthorizedResult), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> DeleteRecipe([FromQuery, Required]int id)
        {
            try
            {
                await _recipesService.DeleteRecipe(id, HttpContext.User.GetPermissions());
                return Ok();
            }
            catch (PermissionException ex)
            {
                return Problem(ex.Value, statusCode:403);
            }
            catch (Exception ex)
            {
                _logger.LogError("An exception happened while processing DeleteRecipe\r\n" + ex);
                return Problem("Unknown error happened while processing your request.", statusCode: 400);
            }
        }

        [HttpPut("uploadImage")]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(UnauthorizedResult), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> UploadImage([FromForm]UploadImageDto uploadImageDtoDto)
        {
            try
            {
                await _recipesService.UploadImage(uploadImageDtoDto.RecipeId, uploadImageDtoDto.File, HttpContext.User.GetPermissions());
                return Ok();
            }
            catch (PermissionException ex)
            {
                return Problem(ex.Value, statusCode:403);
            }
            catch (Exception ex)
            {
                _logger.LogError("An exception happened while processing UploadImage\r\n" + ex);
                return Problem("Unknown error happened while processing your request.", statusCode: 400);
            }
        }
        
    }
}