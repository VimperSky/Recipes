using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Recipes.Domain.Models;
using Recipes.Domain.Repositories;

namespace Recipes.Infrastructure.Repositories
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly RecipesDbContext _recipesDbContext;

        public ActivityRepository(RecipesDbContext recipesDbContext)
        {
            _recipesDbContext = recipesDbContext;
        }
        
        public async Task<Activity> GetOrCreateActivity(int recipeId, int userId)
        {
            var activity = await _recipesDbContext.Activities.FindAsync(recipeId, userId);
            if (activity != null)
                return activity;
            
            var newActivity = new Activity
            {
                RecipeId = recipeId,
                UserId = userId
            };

            await _recipesDbContext.Activities.AddAsync(newActivity);
            return newActivity;
        }

        public async Task<UserActivity> GetUserActivity(int userId)
        {
            var query = _recipesDbContext.Activities.Where(x => x.UserId == userId);
            return new UserActivity
            {
                StarredRecipes = await query.Where(x => x.IsLiked).Select(x => x.RecipeId).ToListAsync(),
                LikedRecipes = await query.Where(x => x.IsLiked).Select(x => x.RecipeId).ToListAsync(),
            };
            // return await _recipesDbContext.Activities
            //     .GroupBy(x => x.UserId)
            //     .Where(x => x.Key == userId)
            //     .Select(g => new UserActivity
            //     {
            //         StarredRecipes = _recipesDbContext.Activities.Where(a => a.IsStarred).Select(a => a.RecipeId).ToList(),
            //         LikesRecipes = _recipesDbContext.Activities.Where(a => a.IsLiked).Select(a => a.RecipeId).ToList()
            //     }).SingleAsync();
        }
    }
}