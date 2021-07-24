namespace Recipes.Domain.Repositories
{
    public interface IAuthRepository
    {
        bool Register(string login, string passwordHash, string name);
    }
}