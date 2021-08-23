using System.Threading.Tasks;
using Recipes.Domain.Models;

namespace Recipes.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User> CreateUser(string login, string passwordHash, string passwordSalt, string name);

        Task<User> GetUserByLogin(string login);

        Task<User> GetUserById(int id);

        Task<UserStats> GetUserStats(int userId);
    }
}