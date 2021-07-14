using System.Linq;
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
    public class RecipesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRecipesRepository _recipesRepository;

        public RecipesController(IUnitOfWork unitOfWork, IRecipesRepository recipesRepository)
        {
            _unitOfWork = unitOfWork;
            _recipesRepository = recipesRepository;
        }
        
        /// <summary>
        /// Retrieve list of recipes
        /// </summary>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="404">Page with this id doesn't exist</response>
        [HttpGet("list")]
        [ProducesResponseType(typeof(RecipePreview[]), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<RecipePreview[]> GetRecipes([FromQuery]string searchString, int page)
        {
            var recipes = _recipesRepository.Get(searchString, page);
            if (recipes == null)
                return NotFound();
            
            return recipes.Select(RecipePreview.FromModel).ToArray();
        }
    }
}