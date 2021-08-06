using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Recipes.Domain;
using Recipes.Domain.Models;
using Recipes.Domain.Repositories;
using Recipes.WebApi.DTO.Recipe;

namespace Recipes.WebApi.Controllers
{   
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class RecipeController: ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRecipesRepository _recipesRepository;
        private readonly IMapper _mapper;

        public RecipeController(IUnitOfWork unitOfWork, IRecipesRepository recipesRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _recipesRepository = recipesRepository;
            _mapper = mapper;
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
            var detail = _recipesRepository.GetById(id);
            if (detail == null)
                return NotFound();
            
            var mappedDetail = _mapper.Map<RecipeDetailDto>(detail);
            return mappedDetail;
        }

        /// <summary>
        /// Create a new recipe
        /// </summary>
        /// <param name="recipeCreateDto"></param>
        /// <returns></returns>
        [HttpPost("create")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(UnauthorizedResult), StatusCodes.Status401Unauthorized)]
        public ActionResult<int> CreateRecipe([FromBody]RecipeCreateDto recipeCreateDto)
        {
            var recipeModel = _mapper.Map<Recipe>(recipeCreateDto);
            var recipe = _recipesRepository.AddRecipe(recipeModel);
            return CreatedAtAction(nameof(GetRecipeDetail), new { id = recipe }, recipe);
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
            var recipeModel = _mapper.Map<Recipe>(recipeEditDto);
            _recipesRepository.EditRecipe(recipeModel);
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
            _recipesRepository.DeleteRecipe(id);
            return NoContent();
        }
        
    }
}