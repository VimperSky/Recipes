namespace Recipes.Application.Exceptions
{
    public class UserAuthenticationException : ExceptionWithValue
    {
        public const string LoginDoesNotExist = "Такой логин не существует.";
        public const string PasswordIsIncorrect = "Пароль неверный.";

        public UserAuthenticationException(string message) : base(message)
        {
        }
    }
}