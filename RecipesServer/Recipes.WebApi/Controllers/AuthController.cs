﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Recipes.Application.Services.Auth;
using Recipes.Application.Services.Auth.Models;
using Recipes.WebApi.DTO.Auth;

namespace Recipes.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class AuthController: ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly AuthService _authService;

        public AuthController(ILogger<AuthController> logger, AuthService authService)
        {
            _logger = logger;
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<string>> Register([FromBody]RegisterDto registerDto)
        {
            try
            {
                return await _authService.Register(registerDto.Login, registerDto.Password, registerDto.Name);
            }
            catch (UserRegistrationException e)
            {
                _logger.LogWarning(e.ToString());
                return Problem(e.Value, statusCode: 409);
            }
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
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<string>> Login([FromBody]LoginDto loginDto)
        {
            try
            {
                return await _authService.Login(loginDto.Login, loginDto.Password);
            }
            catch (UserLoginException e)
            {
                _logger.LogWarning(e.ToString());
                return Problem(e.Value, statusCode: 401);
            }
        }
    }
}