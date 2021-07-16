using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Recipes.Domain;
using Recipes.WebApi.DTO.Recipe;

namespace Recipes.WebApi.Controllers
{   
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class RecipeController: ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public RecipeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        
        /// <summary>
        /// Get detail information about recipe
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="400">Invalid recipe id</response>
        /// <response code="404">Recipe not found</response>
        [HttpGet("detail")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<RecipeDetail> GetRecipeDetail([FromQuery][Required]uint id)
        {
            var detail = _unitOfWork.RecipesRepository.GetById((int)id);
            if (detail == null)
                return NotFound();
            return RecipeDetail.FromModel(detail);
        }
    }
}