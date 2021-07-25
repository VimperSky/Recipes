using System.ComponentModel.DataAnnotations;

namespace Recipes.WebApi.DTO.Auth
{
    public class LoginDto
    {
        [Required]
        public string Login { get; init; }
        
        [Required]
        public string Password { get; init; }
    }
}