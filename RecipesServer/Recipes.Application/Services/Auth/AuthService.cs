using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using Recipes.Application.Exceptions;
using Recipes.Domain;
using Recipes.Domain.Repositories;

namespace Recipes.Application.Services.Auth
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
        /// Register an account. Throws RegisterException on failure
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <param name="name"></param>
        /// <exception cref="UserRegistrationException"></exception>
        public async Task<string> Register(string login, string password, string name)
        {
            if (await _userRepository.GetUser(login) != null)
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
            var user = await _userRepository.GetUser(login);
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
    }
}