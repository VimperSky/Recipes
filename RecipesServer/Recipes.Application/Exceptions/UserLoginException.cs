using System;

namespace Recipes.Application.Exceptions
{
    public class UserLoginException : ExceptionWithValue
    {
        public UserLoginException(string message): base(message) {}

        
        public const string LoginDoesNotExist = "Такой логин не существует";
        public const string PasswordIsIncorrect = "Пароль неверный";
    }
}