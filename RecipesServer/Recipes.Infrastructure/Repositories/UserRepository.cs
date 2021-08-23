using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Recipes.Domain.Models;
using Recipes.Domain.Repositories;

namespace Recipes.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
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

        public async Task<User> GetUserByLogin(string login)
        {
            return await _recipesDbContext.Users.FirstOrDefaultAsync(x => x.Login == login);
        }

        public async Task<User> GetUserById(int id)
        {
            return await _recipesDbContext.Users.FindAsync(id);
        }

        public async Task<UserStats> GetUserStats(int userId)
        {
            var recipesCount = await _recipesDbContext.Recipes.CountAsync(x => x.AuthorId == userId);

            var activities = await _recipesDbContext.Activities
                .Where(a => a.UserId == userId)
                .GroupBy(a => a.UserId)
                .Select(g => new
                {
                    StarsCount = g.Count(x => x.IsStarred),
                    LikesCount = g.Count(x => x.IsLiked)
                }).SingleAsync();
            
            return new UserStats
            {
                RecipesCount = recipesCount,
                StarsCount = activities.StarsCount,
                LikesCount = activities.LikesCount
            };
        }
    }
}