namespace Recipes.Domain.Models
{
    public class User
    {
        public int Id { get; init; }
        public string Login { get; init; }
        public string Password { get; init; }
        public string Name { get; init; }
        
        public string? Bio { get; init; }
    }
}