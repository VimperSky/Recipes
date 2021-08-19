using System;
using System.Security.Claims;
using Recipes.Application.Permissions.Models;

namespace Recipes.Application.Permissions
{
    public static class PermissionsExtensions
    {
        public static UserClaims GetClaims(this ClaimsPrincipal claimsPrincipal)
        {
            string name = null;
            int userId = default;
            foreach (var claim in claimsPrincipal.Claims)
            {
                switch (claim.Type)
                {
                    case CustomClaimTypes.Name:
                        name = claim.Value;
                        break;
                    case CustomClaimTypes.UserId:
                        userId = Convert.ToInt32(claim.Value);
                        break;
                }
            }

            return new UserClaims(name, userId);
        }
    }
}