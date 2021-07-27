using Recipes.Domain.Models;

namespace Recipes.Domain.Repositories
{
    public interface IUserRepository
    {
        void CreateUser(string login, string passwordHash, string passwordSalt, string name);

        User GetUser(string login);
    }
}