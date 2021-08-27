using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Recipes.Application.Permissions;
using Recipes.Application.Services.Activity;
using Recipes.Application.Services.Recipes;
using Recipes.Application.Services.User;
using Recipes.WebApi.DTOs.User;
using Recipes.WebApi.ExceptionHandling;

namespace Recipes.WebApi.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IActivityService _activityService;
        private readonly IRecipesService _recipesService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, 
            IActivityService activityService,
            IRecipesService recipesService,
            IMapper mapper)
        {
            _userService = userService;
            _activityService = activityService;
            _recipesService = recipesService;
            _mapper = mapper;
        }

        /// <summary>
        ///     Register a new user account
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
        public async Task<ActionResult<string>> Register([FromBody] RegisterDto registerDto)
        {
            return await _userService.Register(registerDto.Login, registerDto.Password, registerDto.Name);
        }

        /// <summary>
        ///     Log into an existing account
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
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<string>> Login([FromBody] LoginDto loginDto)
        {
            return await _userService.Login(loginDto.Login, loginDto.Password);
        }
        
        [HttpGet("validateCredentials")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Validate()
        {
            await _userService.ValidateUser(HttpContext.User.GetClaims());
            return Ok();
        }

        /// <summary>
        ///     Get profile info of user
        /// </summary>
        [HttpGet("profile")]
        [ProducesResponseType(typeof(ProfileInfoResultDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ProfileInfoResultDTO>> GetProfileInfo()
        {
            var profileInfo = await _userService.GetProfileInfo(HttpContext.User.GetClaims());
            return _mapper.Map<ProfileInfoResultDTO>(profileInfo);
        }


        /// <summary>
        ///     Set profile info for user
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPatch("profile")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<string>> SetProfileInfo(SetProfileInfo dto)
        {
            return await _userService.SetProfileInfo(dto.Login, dto.Password, dto.Name, dto.Bio,
                HttpContext.User.GetClaims());
        }
        
        /// <summary>
        ///     Get user stats
        /// </summary>
        [HttpGet("stats")]
        [ProducesResponseType(typeof(UserStatsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<UserStatsDto>> GetUserStats()
        {
            var authorClaims = HttpContext.User.GetClaims();
            var activity = await _activityService.GetUserActivitySummary(authorClaims);
            var recipesCount = await _recipesService.GetAuthorRecipesCount(authorClaims);
            return new UserStatsDto
            {
                StarsCount = activity.StarsCount,
                LikesCount = activity.LikesCount,
                RecipesCount = recipesCount
            };
        }

    }
}