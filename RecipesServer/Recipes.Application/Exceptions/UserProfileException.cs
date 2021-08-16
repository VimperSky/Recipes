namespace Recipes.Application.Exceptions
{
    public class UserProfileException: ExceptionWithValue
    {
        public UserProfileException(string message) : base(message)
        {
        }
        
        public const string AccountDoesNotExist = "Аккаунт с заданным ID не существует.";

    }
}