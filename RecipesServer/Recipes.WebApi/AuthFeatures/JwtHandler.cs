using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Recipes.Domain.Models;

namespace Recipes.WebApi.AuthFeatures
{
    public class JwtHandler
    {
        private readonly IConfigurationSection _jwtSettings;
        public JwtHandler(IConfiguration configuration)
        {
            _jwtSettings = configuration.GetSection("JwtSettings");
        }
        private SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(_jwtSettings.GetSection("SecurityKey").Value);
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }
        
        private IEnumerable<Claim> GetClaims(User user)
        {
            var claims = new List<Claim>
            {
                new(nameof(user.Name).ToLower(), user.Name)
            };

            return claims;
        }
        
        public JwtSecurityToken GenerateTokenOptions(User user)
        {
            var tokenOptions = new JwtSecurityToken(
                 _jwtSettings.GetSection("ValidIssuer").Value, 
                 _jwtSettings.GetSection("ValidAudience").Value, 
                 GetClaims(user),
                 expires: DateTime.Now.AddMinutes(Convert.ToDouble(_jwtSettings.GetSection("ExpiryInMinutes").Value)), 
                 signingCredentials: GetSigningCredentials());
            return tokenOptions;
        }
        
    }
}