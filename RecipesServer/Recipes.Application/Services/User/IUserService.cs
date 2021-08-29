using System.Threading.Tasks;
using Recipes.Application.Exceptions;
using Recipes.Application.Models.User;
using Recipes.Application.Permissions.Models;

namespace Recipes.Application.Services.User
{
    public interface IUserService
    {
        /// <summary>
        ///     Register an account. Throws RegisterException on failure
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <param name="name"></param>
        /// <exception cref="UserModificationException"></exception>
        Task<string> Register(string login, string password, string name);

        /// <summary>
        ///     Log into existing account. Returns token on success, throws an LoginException on failure
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        /// <exception cref="UserAuthenticationException"></exception>
        Task<string> Login(string login, string password);

        Task<ProfileInfoResult> GetProfileInfo(UserClaims userClaims);

        /// <summary>
        ///     Set new profile info for a user
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <param name="name"></param>
        /// <param name="bio"></param>
        /// <param name="userClaims"></param>
        /// <returns></returns>
        /// <exception cref="PermissionException"></exception>
        /// <exception cref="UserModificationException"></exception>
        Task<string> SetProfileInfo(string login, string password, string name, string bio, UserClaims userClaims);

        /// <summary>
        ///     Check if UserClaims are valid
        /// </summary>
        /// <param name="userClaims"></param>
        /// <returns></returns>
        Task ValidateUser(UserClaims userClaims);
    }
}