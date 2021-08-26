using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using AutoMapper;
using Recipes.Application.Exceptions;
using Recipes.Application.Models.User;
using Recipes.Application.Permissions.Models;
using Recipes.Domain;
using Recipes.Domain.Repositories;

namespace Recipes.Application.Services.User
{
    public class UserService : IUserService
    {
        private readonly JwtHandler _jwtHandler;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork, JwtHandler jwtHandler,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _jwtHandler = jwtHandler;
            _mapper = mapper;
        }

        /// <summary>
        ///     Register an account. Throws RegisterException on failure
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <param name="name"></param>
        /// <exception cref="UserModificationException"></exception>
        public async Task<string> Register(string login, string password, string name)
        {
            if (await _userRepository.GetUserByLogin(login) != null)
                throw new UserModificationException(UserModificationException.LoginIsTaken);

            var (hash, salt) = HashingTools.QuickHash(password);
            var user = await _userRepository.CreateUser(login, hash, salt, name);
            _unitOfWork.Commit();

            return new JwtSecurityTokenHandler().WriteToken(_jwtHandler.GenerateTokenOptions(user));
        }


        /// <summary>
        ///     Log into existing account. Returns token on success, throws an LoginException on failure
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        /// <exception cref="UserAuthenticationException"></exception>
        public async Task<string> Login(string login, string password)
        {
            var user = await _userRepository.GetUserByLogin(login);
            if (user == null)
                throw new UserAuthenticationException(UserAuthenticationException.LoginDoesNotExist);

            var hashedPassword = HashingTools.HashPassword(password, user.PasswordSalt);
            if (user.PasswordHash != hashedPassword)
                throw new UserAuthenticationException(UserAuthenticationException.PasswordIsIncorrect);

            return new JwtSecurityTokenHandler().WriteToken(_jwtHandler.GenerateTokenOptions(user));
        }

        public async Task<ProfileInfoResult> GetProfileInfo(UserClaims userClaims)
        {
            var user = await _userRepository.GetUserById(userClaims.UserId);
            if (user == null)
                throw new ElementNotFoundException(ElementNotFoundException.AccountDoesNotExist);

            return _mapper.Map<ProfileInfoResult>(user);
        }

        public async Task<string> SetProfileInfo(string login, string password, string name, string bio,
            UserClaims userClaims)
        {
            var user = await _userRepository.GetUserById(userClaims.UserId);
            if (user == null)
                throw new ElementNotFoundException(ElementNotFoundException.AccountDoesNotExist);

            if (login != user.Login && await _userRepository.GetUserByLogin(login) != null)
                throw new UserModificationException(UserModificationException.LoginIsTaken);
            user.Login = login;
            user.Name = name;
            user.Bio = bio;

            var (hash, salt) = HashingTools.QuickHash(password);
            user.PasswordHash = hash;
            user.PasswordSalt = salt;
            _unitOfWork.Commit();

            return new JwtSecurityTokenHandler().WriteToken(_jwtHandler.GenerateTokenOptions(user));
        }
        
        public async Task ValidateUser(UserClaims userClaims)
        {
            if (!userClaims.IsAuthorized)
                throw new UserAuthenticationException(UserAuthenticationException.UserIsInvalid);
            
            var dbUser = await _userRepository.GetUserById(userClaims.UserId);
            if (dbUser == null || dbUser.Name != userClaims.Name)
                throw new UserAuthenticationException(UserAuthenticationException.UserIsInvalid);
        }
    }
}