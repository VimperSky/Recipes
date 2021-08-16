namespace Recipes.WebApi.DTO.User
{
    public class SetUserProfileInfoDto
    {
        public string Name { get; init; }
        
        public string Login { get; init; }
        
        public string Password { get; init; }
        
        public string Bio { get; init; }
    }
}