using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Recipes.Application.DTOs.User;
using Recipes.Application.Permissions;
using Recipes.Application.Services.User;
using Recipes.WebApi.DTO.User;
using Recipes.WebApi.ExceptionHandling;

namespace Recipes.WebApi.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class UserController: ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
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
        [AllowAnonymous]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status409Conflict)]
        public async Task<ActionResult<string>> Register([FromBody]RegisterDto registerDto)
        {
            return await _userService.Register(registerDto.Login, registerDto.Password, registerDto.Name);
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
        [AllowAnonymous]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorDetails),StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<string>> Login([FromBody]LoginDto loginDto)
        {
            return await _userService.Login(loginDto.Login, loginDto.Password);
        }
        
        
        /// <summary>
        /// Get Profile Info
        /// </summary>
        [HttpGet("profile")] 
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails),StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<UserProfileDto>> GetProfile()
        {
            return await _userService.GetUserProfile(HttpContext.User.GetClaims());
        }
    }
}