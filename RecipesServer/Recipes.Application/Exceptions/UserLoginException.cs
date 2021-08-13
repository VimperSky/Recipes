using System;

namespace Recipes.Application.Exceptions
{
    public class UserLoginException : Exception
    {
        public string Value { get; }
        public UserLoginException(string message): base("UserLoginException: " + message)
        {
            Value = message;
        }
        
        public const string LoginDoesNotExist = "Такой логин не существует";
        public const string PasswordIsIncorrect = "Пароль неверный";
    }
}