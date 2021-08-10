using System;

namespace Recipes.Application.Permissions.Models
{
    public class PermissionException : Exception
    {
        public string Value { get; }
        public PermissionException(string message): base($"PermissionException: {message}")
        {
            Value = message;
        }
        
        public const string NotEnoughPermissionsToModifyRecipe = "You don't have permission to modify this recipe";
    }
}