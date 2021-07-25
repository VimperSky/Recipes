using System;
using System.Linq;
using Recipes.Domain.Models;
using Recipes.Domain.Repositories;

namespace Recipes.Infrastructure.Repositories
{
    public class AuthRepository: IAuthRepository
    {
        private readonly RecipesDbContext _recipesDbContext;

        public AuthRepository(RecipesDbContext recipesDbContext)
        {
            _recipesDbContext = recipesDbContext;
        }
        
        public bool Register(string login, string password, string name)
        {
            if (string.IsNullOrWhiteSpace(login))
                throw new ArgumentNullException(login);

            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException(password);

            if (_recipesDbContext.Users.Any(x => x.Login == login))
                return false;

            _recipesDbContext.Users.Add(new User {Login = login, Password = password, Name = name});
            return true;
        }

        public User Login(string login, string password)
        {
            if (string.IsNullOrWhiteSpace(login))
                throw new ArgumentNullException(login);

            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException(password);

            var targetUser = _recipesDbContext.Users.FirstOrDefault(x => x.Login == login);
            if (targetUser == null || password != targetUser.Password)
                return null;

            return targetUser;
        }
    }
}