using System;

namespace Recipes.WebApi.AuthFeatures.Models
{
    public class RegisterException : Exception
    {
        public RegisterException(string message): base(message)
        {
            
        }
    }

}