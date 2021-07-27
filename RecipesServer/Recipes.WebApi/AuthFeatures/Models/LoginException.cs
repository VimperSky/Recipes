using System;

namespace Recipes.WebApi.AuthFeatures.Models
{
    public class LoginException : Exception
    {
        public LoginException(string message): base(message)
        {
            
        }
    }
}