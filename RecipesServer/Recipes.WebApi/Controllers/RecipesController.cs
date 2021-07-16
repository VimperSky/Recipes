using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Recipes.Domain;
using Recipes.WebApi.DTO;
using Recipes.WebApi.DTO.Recipe;

namespace Recipes.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class RecipesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public RecipesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        /// <summary>
        /// Retrieve a list of recipes
        /// </summary>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="400">Invalid page id</response>
        /// <response code="404">Page with this id doesn't exist</response>
        [HttpGet("list")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<RecipesPage> GetRecipeList([FromQuery]uint page, [FromQuery]string searchString)
        {
            if (page == default)
                page = 1;
            
            var (values, hasMore) = _unitOfWork.RecipesRepository.GetPage((int)page, searchString);
            if (values == null)
                return NotFound();

            var recipesPage = new RecipesPage
            {
                Recipes = values.Select(RecipePreview.FromModel).ToArray(),
                HasMore = hasMore
            };
            
            return recipesPage;
        }
    }
}