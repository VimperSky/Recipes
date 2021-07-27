using System;
using System.Linq;
using Recipes.Domain.Models;
using Recipes.Domain.Repositories;

namespace Recipes.Infrastructure.Repositories
{
    public class UserRepository: IUserRepository
    {
        private readonly RecipesDbContext _recipesDbContext;

        public UserRepository(RecipesDbContext recipesDbContext)
        {
            _recipesDbContext = recipesDbContext;
        }
        
        public void CreateUser(string login, string passwordHash, string passwordSalt, string name)
        {
            _recipesDbContext.Users.Add(new User
            {
                Login = login,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Name = name
            });
        }

        public User GetUser(string login)
        {
            return _recipesDbContext.Users.FirstOrDefault(x => x.Login == login);
        }
    }
}