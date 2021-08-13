using System;

namespace Recipes.Application.Exceptions
{
    public class PermissionException : Exception
    {
        public string Value { get; }
        public PermissionException(string message): base($"PermissionException: {message}")
        {
            Value = message;
        }
        
        public const string NotEnoughPermissionsToModifyResource = "У вас нет прав на модификацию данного ресурса.";
    }
}