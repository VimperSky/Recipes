using System.Threading.Tasks;
using Recipes.Application.Exceptions;

namespace Recipes.Application.Services.Auth
{
    public interface IAuthService
    {
        /// <summary>
        /// Register an account. Throws RegisterException on failure
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <param name="name"></param>
        /// <exception cref="UserRegistrationException"></exception>
        Task<string> Register(string login, string password, string name);

        /// <summary>
        /// Log into existing account. Returns token on success, throws an LoginException on failure
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        /// <exception cref="UserLoginException"></exception>
        Task<string> Login(string login, string password);
    }
}