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
        
        public const string LoginDoesNotExist = "The login doesn't exist in database";
        public const string PasswordIsIncorrect = "Incorrect password for the login";
    }
}