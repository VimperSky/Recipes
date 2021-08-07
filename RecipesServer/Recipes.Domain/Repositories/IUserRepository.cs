using System.Threading.Tasks;
using Recipes.Domain.Models;

namespace Recipes.Domain.Repositories
{
    public interface IUserRepository
    {
        Task CreateUser(string login, string passwordHash, string passwordSalt, string name);

        Task<User> GetUser(string login);
    }
}