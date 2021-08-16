using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Recipes.Application.Services.User
{
    public static class HashingTools
    {
        private const int Iterations = 10000;
        private const int BytesRequested = 256 / 8;

        /// <summary>
        ///     Hashed password with predefined salt
        /// </summary>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        public static string HashPassword(string password, string salt)
        {
            return HashPassword(password, StringToSalt(salt));
        }

        /// <summary>
        ///     Hashed password with automatically generated salt and returns both hash and salt
        /// </summary>
        /// <param name="password"></param>
        /// <returns>string Hash, string Salt</returns>
        public static (string Hash, string Salt) QuickHash(string password)
        {
            var salt = GenerateSalt();
            var hash = HashPassword(password, salt);
            return (hash, SaltToString(salt));
        }

        private static byte[] GenerateSalt()
        {
            var salt = new byte[128 / 8];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(salt);

            return salt;
        }

        private static string HashPassword(string password, byte[] salt)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(password, salt,
                KeyDerivationPrf.HMACSHA1, Iterations, BytesRequested));
        }

        private static byte[] StringToSalt(string saltString)
        {
            return Convert.FromBase64String(saltString);
        }

        private static string SaltToString(byte[] salt)
        {
            return Convert.ToBase64String(salt);
        }
    }
}