using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Recipes.Domain;
using Recipes.Domain.Repositories;
using Recipes.WebApi.DTO.Auth;

namespace Recipes.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class AuthController: ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthRepository _authRepository;

        public AuthController(IMapper mapper, IUnitOfWork unitOfWork, IAuthRepository authRepository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _authRepository = authRepository;
        }

        /// <summary>
        /// Register a new user account
        /// </summary>
        /// <param name="register"></param>
        /// <response code="200">Account registered</response>
        /// <response code="403">Login is taken</response>
        /// <returns></returns>
        [HttpPost("register")] 
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult RegisterUser([FromBody]Register register)
        {
            if (_authRepository.Register(register.Login, register.PasswordHash, register.Name))
            {
                _unitOfWork.Commit();
                return Ok();
            }

            return Forbid();
        }
    }
}