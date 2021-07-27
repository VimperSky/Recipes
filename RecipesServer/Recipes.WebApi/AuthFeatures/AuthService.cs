using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Recipes.Domain;
using Recipes.Domain.Repositories;
using Recipes.WebApi.AuthFeatures.Models;
using Recipes.WebApi.DTO.Auth;

namespace Recipes.WebApi.AuthFeatures
{
    public class AuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly JwtHandler _jwtHandler;

        public AuthService(IUserRepository userRepository, IUnitOfWork unitOfWork, JwtHandler jwtHandler)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _jwtHandler = jwtHandler;
        }

        
        /// <summary>
        /// Register an account. Throws ArgumentException or RegisterException on failure
        /// </summary>
        /// <param name="registerDto"></param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="RegisterException"></exception>
        public void Register(RegisterDto registerDto)
        {
            var login = registerDto.Login;
            if (string.IsNullOrWhiteSpace(login) || login.Length < 3)
                throw new ArgumentException("Login must be at least 3 characters long");
            
            var password = registerDto.Password;
            if (string.IsNullOrWhiteSpace(password) || password.Length < 8)
                throw new ArgumentException("Password must be at least 8 characters long");

            var name = registerDto.Name;
            if (name == null)
                throw new ArgumentException("Name must be not null");

            var user = _userRepository.GetUser(login);
            if (user != null)
                throw new RegisterException("Login is already taken");

            var salt = HashingTools.GenerateSalt();
            var hash = HashingTools.HashPassword(password, salt);
            _userRepository.CreateUser(login, hash, HashingTools.SaltToString(salt), name);
            _unitOfWork.Commit();
        }
        
        /// <summary>
        /// Log into existing account. Returns token on success, throws an ArgumentException or LoginException on failure
        /// </summary>
        /// <param name="loginDto"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="LoginException"></exception>
        public string Login(LoginDto loginDto)
        {
            var login = loginDto.Login;
            if (string.IsNullOrWhiteSpace(login))
                throw new ArgumentException("Login cannot be null or empty");

            var password = loginDto.Password;
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password cannot be null or empty");
            
            var user = _userRepository.GetUser(login);
            if (user == null)
                throw new LoginException("Such login doesn't exist in database");

            var salt = HashingTools.StringToSalt(user.PasswordSalt);
            var hashedPassword = HashingTools.HashPassword(password, salt);
            if (user.PasswordHash != hashedPassword)
                throw new LoginException("Invalid password for the login");
            
            var tokenOptions = _jwtHandler.GenerateTokenOptions(user);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return token;
        }
    }
}