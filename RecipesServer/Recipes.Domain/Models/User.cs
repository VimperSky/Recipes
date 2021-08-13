namespace Recipes.Domain.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; init; }
        public string PasswordHash { get; init; }
        public string PasswordSalt { get; init; }
        public string Name { get; init; }
        
        public string Bio { get; init; }
    }
}