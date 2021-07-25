using Recipes.Domain.Models;

namespace Recipes.Domain.Repositories
{
    public interface IAuthRepository
    {
        bool Register(string login, string password, string name);

        User Login(string login, string password);
    }
}