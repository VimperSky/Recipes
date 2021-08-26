using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Recipes.Application.Models.Recipe;
using Recipes.Application.Permissions;
using Recipes.Application.Services.Recipes;
using Recipes.WebApi.DTOs.Recipe;
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
        private readonly IMapper _mapper;

        public RecipeController(IRecipesService recipesService, IMapper mapper)
        {
            _recipesService = recipesService;
            _mapper = mapper;
        }


        /// <summary>
        ///     Get detail information about recipe
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("detail")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(RecipeDetailResultResultDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RecipeDetailResultResultDTO>> GetRecipeDetail(
            [FromQuery] [Required] [Range(1, int.MaxValue)] int id)
        {
            var detail = await _recipesService.GetRecipeDetail(id, HttpContext.User.GetClaims());
            if (detail == null)
                return NotFound();

            return _mapper.Map<RecipeDetailResultResultDTO>(detail);
        }

        /// <summary>
        ///     Create a new recipe
        /// </summary>
        /// <param name="recipeCreateRequestDTO"></param>
        /// <returns></returns>
        [HttpPost("create")]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<int>> CreateRecipe([FromBody] RecipeCreateRequestDTO recipeCreateRequestDTO)
        {
            var recipeId = await _recipesService.CreateRecipe(_mapper.Map<RecipeCreateCommand>(recipeCreateRequestDTO), 
                HttpContext.User.GetClaims());
            return CreatedAtAction(nameof(GetRecipeDetail), new { id = recipeId }, recipeId);
        }

        /// <summary>
        ///     Edit an existing recipe
        /// </summary>
        /// <param name="recipeEditRequestDTO"></param>
        /// <returns></returns>
        [HttpPatch("edit")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> EditRecipe([FromBody] RecipeEditRequestDTO recipeEditRequestDTO)
        {
            await _recipesService.EditRecipe(_mapper.Map<RecipeEditCommand>(recipeEditRequestDTO),
                HttpContext.User.GetClaims());
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
        /// <param name="uploadImageRequestDTO"></param>
        /// <returns></returns>
        [HttpPut("uploadImage")]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UploadImage([FromForm] UploadImageRequestDTO uploadImageRequestDTO)
        {
            await _recipesService.UploadImage(uploadImageRequestDTO.RecipeId, uploadImageRequestDTO.File,
                HttpContext.User.GetClaims());
            return Ok();
        }
    }
}