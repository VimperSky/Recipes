using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Recipes.Application.DTOs.Recipe;
using Recipes.Application.Permissions;
using Recipes.Application.Services.Recipes;
using Recipes.WebApi.DTO.Recipe;
using Recipes.WebApi.ExceptionHandling;

namespace Recipes.WebApi.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipesService _recipesService;

        public RecipeController(IRecipesService recipesService)
        {
            _recipesService = recipesService;
        }


        /// <summary>
        ///     Get detail information about recipe
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("detail")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(RecipeDetailDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RecipeDetailDto>> GetRecipeDetail(
            [FromQuery] [Required] [Range(1, int.MaxValue)] int id)
        {
            var detail = await _recipesService.GetRecipeDetail(id);
            if (detail == null)
                return NotFound();

            return detail;
        }

        /// <summary>
        ///     Create a new recipe
        /// </summary>
        /// <param name="recipeCreateDto"></param>
        /// <returns></returns>
        [HttpPost("create")]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<int>> CreateRecipe([FromBody] RecipeCreateDto recipeCreateDto)
        {
            var recipeId = await _recipesService.CreateRecipe(recipeCreateDto, HttpContext.User.GetClaims());
            return CreatedAtAction(nameof(GetRecipeDetail), new { id = recipeId }, recipeId);
        }

        /// <summary>
        ///     Edit an existing recipe
        /// </summary>
        /// <param name="recipeEditDto"></param>
        /// <returns></returns>
        [HttpPatch("edit")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> EditRecipe([FromBody] RecipeEditDto recipeEditDto)
        {
            await _recipesService.EditRecipe(recipeEditDto, HttpContext.User.GetClaims());
            return Ok();
        }

        /// <summary>
        ///     Delete an existing recipe
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteRecipe([FromQuery] [Required] int id)
        {
            await _recipesService.DeleteRecipe(id, HttpContext.User.GetClaims());
            return Ok();
        }

        /// <summary>
        ///     Upload image for the recipe
        /// </summary>
        /// <param name="uploadImageDtoDto"></param>
        /// <returns></returns>
        [HttpPut("uploadImage")]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UploadImage([FromForm] UploadImageDto uploadImageDtoDto)
        {
            await _recipesService.UploadImage(uploadImageDtoDto.RecipeId, uploadImageDtoDto.File,
                HttpContext.User.GetClaims());
            return Ok();
        }
    }
}