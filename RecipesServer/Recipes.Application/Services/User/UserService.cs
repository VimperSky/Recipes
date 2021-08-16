using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using AutoMapper;
using Recipes.Application.DTOs.User;
using Recipes.Application.Exceptions;
using Recipes.Application.Permissions.Models;
using Recipes.Domain;
using Recipes.Domain.Repositories;

namespace Recipes.Application.Services.User
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly JwtHandler _jwtHandler;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork, JwtHandler jwtHandler, IMapper mapper)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _jwtHandler = jwtHandler;
            _mapper = mapper;
        }

        
        /// <summary>
        /// Register an account. Throws RegisterException on failure
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <param name="name"></param>
        /// <exception cref="UserRegistrationException"></exception>
        public async Task<string> Register(string login, string password, string name)
        {
            if (await _userRepository.GetUserByLogin(login) != null)
                throw new UserRegistrationException(UserRegistrationException.LoginIsTaken);

            var salt = HashingTools.GenerateSalt();
            var hash = HashingTools.HashPassword(password, salt);
            var user = await _userRepository.CreateUser(login, hash, HashingTools.SaltToString(salt), name);
            _unitOfWork.Commit();
            
            var tokenOptions = _jwtHandler.GenerateTokenOptions(user);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return token;
        }
        

        /// <summary>
        /// Log into existing account. Returns token on success, throws an LoginException on failure
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        /// <exception cref="UserLoginException"></exception>
        public async Task<string> Login(string login, string password)
        {
            var user = await _userRepository.GetUserByLogin(login);
            if (user == null)
                throw new UserLoginException(UserLoginException.LoginDoesNotExist);

            var salt = HashingTools.StringToSalt(user.PasswordSalt);
            var hashedPassword = HashingTools.HashPassword(password, salt);
            if (user.PasswordHash != hashedPassword)
                throw new UserLoginException(UserLoginException.PasswordIsIncorrect);
            
            var tokenOptions = _jwtHandler.GenerateTokenOptions(user);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return token;
        }

        public async Task<UserProfileDto> GetUserProfile(UserClaims userClaims)
        {
            var user = await _userRepository.GetUserById(userClaims.UserId);
            if (user == null)
                throw new UserProfileException(UserProfileException.AccountDoesNotExist);

            return _mapper.Map<UserProfileDto>(user);
        }
    }
}