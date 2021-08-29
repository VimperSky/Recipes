using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class RecipesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRecipesService _recipesService;

        public RecipesController(IRecipesService recipesService, IMapper mapper)
        {
            _recipesService = recipesService;
            _mapper = mapper;
        }

        /// <summary>
        ///     Retrieve a list of recipes
        /// </summary>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="400">Invalid input data</response>
        /// <response code="404">Page with this id doesn't exist</response>
        [HttpGet("list")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(RecipesPageResultDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<RecipesPageResultDTO>> GetRecipes(
            [FromQuery, Required, Range(1, int.MaxValue)] int pageSize,
            [FromQuery, Range(1, int.MaxValue)] int page = 1, 
            [FromQuery] string searchString = null)
        {
            var recipesPage = await _recipesService.GetRecipesPage(pageSize, page,
                RecipesSelectionType.All, HttpContext.User.GetClaims(), searchString);
            return _mapper.Map<RecipesPageResultDTO>(recipesPage);
        }


        [HttpGet("myList")]
        [ProducesResponseType(typeof(RecipesPageResultDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<RecipesPageResultDTO>> GetMyRecipes(
            [FromQuery, Required, Range(1, int.MaxValue)] int pageSize,
            [FromQuery, Range(1, int.MaxValue)] int page = 1)
        {
            var recipesPage = await _recipesService.GetRecipesPage(pageSize, page, RecipesSelectionType.Own,
                HttpContext.User.GetClaims());
            return _mapper.Map<RecipesPageResultDTO>(recipesPage);
        }

        [HttpGet("favoriteList")]
        [ProducesResponseType(typeof(RecipesPageResultDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<RecipesPageResultDTO>> GetFavoriteRecipes(
            [FromQuery, Required, Range(1, int.MaxValue)] int pageSize,
            [FromQuery, Range(1, int.MaxValue)] int page = 1)
        {
            var recipesPage = await _recipesService.GetRecipesPage(pageSize, page, RecipesSelectionType.Starred,
                HttpContext.User.GetClaims());
            return _mapper.Map<RecipesPageResultDTO>(recipesPage);
        }

        [ProducesResponseType(typeof(RecipePreviewResultDTO), StatusCodes.Status200OK)]
        [AllowAnonymous]
        [HttpGet("recipeOfDay")]
        public async Task<ActionResult<RecipePreviewResultDTO>> GetRecipeOfTheDay()
        {
            var recipePreview = await _recipesService.GetRecipeOfTheDay();
            return _mapper.Map<RecipePreviewResultDTO>(recipePreview);
        }
    }
}