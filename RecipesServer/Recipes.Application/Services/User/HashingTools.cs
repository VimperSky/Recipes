using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Recipes.Application.Services.User
{
    public static class HashingTools
    {
        private const int Iterations = 10000;
        private const int BytesRequested = 256 / 8;
        public static byte[] GenerateSalt()
        {
            var salt = new byte[128 / 8];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(salt);

            return salt;
        }

        public static byte[] StringToSalt(string saltString)
        {
            return Convert.FromBase64String(saltString);
        }

        public static string SaltToString(byte[] salt)
        {
            return Convert.ToBase64String(salt);
        }
        
        public static string HashPassword(string password, byte[] salt)
        {
            var hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(password, salt,
                KeyDerivationPrf.HMACSHA1, Iterations, BytesRequested));

            return hash;
        }
    }
}