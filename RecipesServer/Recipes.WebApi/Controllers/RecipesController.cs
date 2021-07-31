using System.ComponentModel.DataAnnotations;
using System.Linq;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public RecipesController(IUnitOfWork unitOfWork, IRecipesRepository recipesRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _recipesRepository = recipesRepository;
            _mapper = mapper;
        }
        
        /// <summary>
        /// Retrieve a list of recipes
        /// </summary>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="400">Invalid input data</response>
        /// <response code="404">Page with this id doesn't exist</response>
        [HttpGet("list")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<RecipesPageDto> GetRecipes([FromQuery][Required, Range(1, int.MaxValue)]int pageSize, 
            [FromQuery][Range(1, int.MaxValue)]int page = 1, [FromQuery]string searchString = "")
        {
            var pageCount = _recipesRepository.GetPagesCount(pageSize, searchString);
            if (page > 1 && page > pageCount)
                return NotFound();

            var recipes = _recipesRepository.GetPage(page, pageSize, searchString);

            var recipesPage = new RecipesPageDto
            {
                Recipes = _mapper.Map<RecipePreviewDto[]>(recipes),
                PageCount = pageCount
            };
            
            return recipesPage;
        }
    }
}