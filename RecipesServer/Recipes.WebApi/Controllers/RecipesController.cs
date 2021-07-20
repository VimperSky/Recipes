using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Recipes.Domain;
using Recipes.Domain.Repositories;
using Recipes.WebApi.DTO;
using Recipes.WebApi.DTO.Recipe;

namespace Recipes.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
        public ActionResult<RecipesPage> GetRecipes([FromQuery][Required]int pageSize, [FromQuery]int page = 1,
            [FromQuery]string searchString = "")
        {
            if (pageSize <= 0)
                return BadRequest();

            if (page <= 0)
                return BadRequest();

            var pageCount = _recipesRepository.GetPagesCount(pageSize, searchString);
            if (page > 1 && page > pageCount)
                return NotFound();

            var recipes = _recipesRepository.GetPage(page, pageSize, searchString);

            var recipesPage = new RecipesPage
            {
                Recipes = recipes.Select(RecipePreview.FromModel).ToArray(),
                PageCount = pageCount
            };
            
            return recipesPage;
        }
    }
}