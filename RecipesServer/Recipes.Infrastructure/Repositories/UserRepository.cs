using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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
        
        public async Task<User> CreateUser(string login, string passwordHash, string passwordSalt, string name)
        {
            var newUser = new User
            {
                Login = login,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Name = name
            };
            await _recipesDbContext.Users.AddAsync(newUser);
            return newUser;
        }

        public async Task<User> GetUser(string login)
        {
            return await _recipesDbContext.Users.FirstOrDefaultAsync(x => x.Login == login);
        }
    }
}