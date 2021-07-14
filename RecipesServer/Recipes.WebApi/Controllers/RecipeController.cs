using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Recipes.Domain;
using Recipes.Domain.Repositories;
using Recipes.WebApi.DTO.Recipe;

namespace Recipes.WebApi.Controllers
{   
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class RecipeController: ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRecipesRepository _recipesRepository;

        public RecipeController(IUnitOfWork unitOfWork, IRecipesRepository recipesRepository)
        {
            _unitOfWork = unitOfWork;
            _recipesRepository = recipesRepository;
        }

        
        /// <summary>
        /// Retrieves detail about recipe
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="404">Recipe not found</response>
        [HttpGet("detail")]
        [ProducesResponseType(typeof(RecipeDetail), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<RecipeDetail> Detail([FromQuery]int id)
        {
            var detail = _recipesRepository.GetById(id);
            if (detail == null)
                return NotFound();
            return RecipeDetail.FromModel(detail);
        }
    }
}