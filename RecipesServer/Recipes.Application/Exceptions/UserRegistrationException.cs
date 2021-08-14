namespace Recipes.Application.Exceptions
{
    public class UserRegistrationException : ExceptionWithValue
    {
        public UserRegistrationException(string message): base(message) {}
        
        public const string LoginIsTaken = "Данный логин занят.";
    }

}