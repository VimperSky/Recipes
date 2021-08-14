using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Recipes.Application.Services.Auth;
using Recipes.WebApi.DTO.Auth;
using Recipes.WebApi.ExceptionHandling;

namespace Recipes.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class AuthController: ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Register a new user account
        /// </summary>
        /// <param name="registerDto"></param>
        /// <response code="200">Successfully registered</response>
        /// <response code="400">Invalid input data</response>
        /// <response code="409">Cannot register account with the specified data</response>
        /// <returns></returns>
        [HttpPost("register")] 
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status409Conflict)]
        public async Task<ActionResult<string>> Register([FromBody]RegisterDto registerDto)
        {
            return await _authService.Register(registerDto.Login, registerDto.Password, registerDto.Name);
        }
        
        /// <summary>
        /// Log into an existing account
        /// </summary>
        /// <param name="loginDto"></param>
        /// <response code="200">Successfully logged in</response>
        /// <response code="400">Invalid input data</response>
        /// <response code="401">Invalid credentials</response>
        /// <returns></returns>
        [HttpPost("login")] 
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorDetails),StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<string>> Login([FromBody]LoginDto loginDto)
        {
            return await _authService.Login(loginDto.Login, loginDto.Password);
        }
    }
}