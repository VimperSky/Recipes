using System;

namespace Recipes.Application.Services.Auth.Models
{
    public class UserRegistrationException : Exception
    {
        public UserRegistrationException(string message): base(message)
        {
            
        }
        
        public const string LoginIsTaken = "Login is already taken";
    }

}