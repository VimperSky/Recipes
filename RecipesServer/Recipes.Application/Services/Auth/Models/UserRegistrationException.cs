using System;

namespace Recipes.Application.Services.Auth.Models
{
    public class UserRegistrationException : Exception
    {
        public string Value { get; }
        public UserRegistrationException(string message): base("UserRegistrationException: " + message)
        {
            Value = message;
        }
        
        public const string LoginIsTaken = "Login is already taken";
    }

}