using Microsoft.AspNetCore.Mvc;
using Recipes.WebApi.DTO.Auth;

namespace Recipes.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class AuthController: ControllerBase
    {
        [HttpPost("Registration")] 
        public IActionResult RegisterUser([FromBody] Register register)
        {
            
            return Ok();
        }
    }
}