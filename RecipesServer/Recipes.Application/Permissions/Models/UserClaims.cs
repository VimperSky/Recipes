using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Recipes.Application.Permissions.Models
{
    public class UserClaims
    {
        public string Name { get; }
        public int UserId { get; }

        public UserClaims(IEnumerable<Claim> claims)
        {
            foreach (var claim in claims)
            {
                if (claim.Type == CustomClaimTypes.Name)
                    Name = claim.Value;
                
                if (claim.Type == CustomClaimTypes.UserId)
                    UserId = Convert.ToInt32(claim.Value);
            }
        }
    }
}