using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Recipes.Application.Permissions.Models;

namespace Recipes.Application.Services.User
{
    public class JwtHandler
    {
        private readonly IOptions<JwtSettings> _jwtSettings;

        public JwtHandler(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings;
        }

        private SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(_jwtSettings.Value.SecurityKey);
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private IEnumerable<Claim> GetClaims(Domain.Models.User user)
        {
            var claims = new List<Claim>
            {
                new(CustomClaimTypes.Name, user.Name),
                new(CustomClaimTypes.UserId, user.Id.ToString())
            };

            return claims;
        }

        public JwtSecurityToken GenerateTokenOptions(Domain.Models.User user)
        {
            var tokenOptions = new JwtSecurityToken(
                _jwtSettings.Value.ValidIssuer,
                _jwtSettings.Value.ValidAudience,
                GetClaims(user),
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_jwtSettings.Value.ExpiryInMinutes)),
                signingCredentials: GetSigningCredentials());
            return tokenOptions;
        }
    }
}