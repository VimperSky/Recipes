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
        
        public bool Register(string login, string passwordHash, string name)
        {
            if (string.IsNullOrWhiteSpace(login))
                throw new ArgumentNullException(login);

            if (string.IsNullOrWhiteSpace(passwordHash))
                throw new ArgumentNullException(passwordHash);

            if (_recipesDbContext.Users.Any(x => x.Login == login))
                return false;

            _recipesDbContext.Users.Add(new User {Login = login, PasswordHash = passwordHash, Name = name});
            return true;
        }
    }
}