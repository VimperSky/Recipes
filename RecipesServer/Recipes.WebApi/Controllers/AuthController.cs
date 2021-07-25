using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Recipes.Domain;
using Recipes.Domain.Repositories;
using Recipes.WebApi.AuthFeatures;
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
        private readonly JwtHandler _jwtHandler;

        public AuthController(IMapper mapper, IUnitOfWork unitOfWork, IAuthRepository authRepository, JwtHandler jwtHandler)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _authRepository = authRepository;
            _jwtHandler = jwtHandler;
        }

        /// <summary>
        /// Register a new user account
        /// </summary>
        /// <param name="registerDto"></param>
        /// <response code="200">Successfully registered</response>
        /// <response code="409">Login is taken</response>
        /// <returns></returns>
        [HttpPost("register")] 
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public IActionResult Register([FromBody]RegisterDto registerDto)
        {
            if (!_authRepository.Register(registerDto.Login, registerDto.Password, registerDto.Name))
            {
                return Conflict();
            }
            
            _unitOfWork.Commit();
            return Ok();
        }
        
        /// <summary>
        /// Log into an existing account
        /// </summary>
        /// <param name="loginDto"></param>
        /// <response code="200">Successfully logged in</response>
        /// <response code="401">Invalid credentials</response>
        /// <returns></returns>
        [HttpPost("login")] 
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<string> Login([FromBody]LoginDto loginDto)
        {
            var user = _authRepository.Login(loginDto.Login, loginDto.Password);
            if (user == null)
                return Unauthorized();
            
            var tokenOptions = _jwtHandler.GenerateTokenOptions(user);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions); 
            return token;
        }
    }
}