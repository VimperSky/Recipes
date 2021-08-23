namespace Recipes.Application.Permissions.Models
{
    public class UserClaims
    {
        public UserClaims(string name, int userId)
        {
            Name = name;
            UserId = userId;
        }

        public string Name { get; }
        public int UserId { get; }

        public bool IsAuthorized => UserId != default;
    }
}