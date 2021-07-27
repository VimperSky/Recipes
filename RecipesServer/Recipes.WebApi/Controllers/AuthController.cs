using System;
using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Recipes.Domain;
using Recipes.Domain.Repositories;
using Recipes.WebApi.AuthFeatures;
using Recipes.WebApi.AuthFeatures.Models;
using Recipes.WebApi.DTO.Auth;

namespace Recipes.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class AuthController: ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<AuthController> _logger;
        private readonly AuthService _authService;
        private readonly JwtHandler _jwtHandler;

        public AuthController(IMapper mapper, ILogger<AuthController> logger, AuthService authService, JwtHandler jwtHandler)
        {
            _mapper = mapper;
            _logger = logger;
            _authService = authService;
            _jwtHandler = jwtHandler;
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
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
        public ActionResult Register([FromBody]RegisterDto registerDto)
        {
            try
            {
                _authService.Register(registerDto);
                return Ok();
            }
            catch (ArgumentException e)
            {
                _logger.LogWarning(e.ToString());
                return BadRequest(e.Message);
            }
            catch (RegisterException e)
            {
                _logger.LogWarning(e.ToString());
                return Conflict(e.Message);
            }
            catch (Exception e)
            {
                _logger.LogWarning("Unhandled register exception: " + e);
                return BadRequest("Unknown register exception happened");
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<string> Login([FromBody]LoginDto loginDto)
        {
            try
            {
                var loginToken = _authService.Login(loginDto);
                return loginToken;
            }
            catch (ArgumentException e)
            {
                _logger.LogWarning(e.ToString());
                return BadRequest(e.Message);
            }
            catch (LoginException e)
            {
                _logger.LogWarning(e.ToString());
                return Unauthorized(e.Message);
            }
            catch (Exception e)
            {
                _logger.LogWarning("Unhandled login exception: " + e);
                return BadRequest("Unknown login exception happened");
            }
        }
    }
}