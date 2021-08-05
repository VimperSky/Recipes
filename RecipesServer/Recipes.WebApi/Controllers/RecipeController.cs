using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Recipes.Domain;
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
    }
}