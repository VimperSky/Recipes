namespace Recipes.Application.Exceptions
{
    public class UserModificationException : ExceptionWithValue
    {
        public const string LoginIsTaken = "Данный логин занят.";

        public UserModificationException(string message) : base(message)
        {
        }
    }
}