using System.Security.Claims;
using Recipes.Application.Permissions.Models;

namespace Recipes.Application.Permissions
{
    public static class PermissionsExtensions
    {
        public static UserClaims GetClaims(this ClaimsPrincipal claimsPrincipal)
        {
            return new UserClaims(claimsPrincipal.Claims);
        }
    }
}