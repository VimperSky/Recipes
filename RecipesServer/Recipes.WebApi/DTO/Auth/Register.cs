using System.ComponentModel.DataAnnotations;

namespace Recipes.WebApi.DTO.Auth
{
    public class Register
    {
        [Required]
        public string Name { get; init; }
        
        [Required]
        public string Login { get; init; }
        
        [Required]
        public string PasswordHash { get; init; }
    }
}