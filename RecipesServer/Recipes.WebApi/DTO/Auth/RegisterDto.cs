using System.ComponentModel.DataAnnotations;

namespace Recipes.WebApi.DTO.Auth
{
    public class RegisterDto
    {
        [Required]
        public string Name { get; init; }
        
        [Required]
        public string Login { get; init; }
        
        [Required]
        public string Password { get; init; }
    }
}