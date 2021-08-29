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
        
        public async Task<UserRecipesActivityResult> GetUserRecipesActivity(int userId, int[] recipeIds)
        {
            var query = _recipesDbContext.Activities.Where(x => x.UserId == userId
                                                                && recipeIds.Contains(x.RecipeId));
            return new UserRecipesActivityResult
            {
                StarredRecipes = await query.Where(x => x.IsStarred).Select(x => x.RecipeId).ToListAsync(),
                LikedRecipes = await query.Where(x => x.IsLiked).Select(x => x.RecipeId).ToListAsync(),
            };
        }

        public async Task<UserActivitySummary> GetUserActivitySummary(int userId)
        {
            return await _recipesDbContext.Activities
                .Where(a => a.UserId == userId)
                .GroupBy(a => a.UserId)
                .Select(g => new UserActivitySummary
                {
                    StarsCount = g.Count(x => x.IsStarred),
                    LikesCount = g.Count(x => x.IsLiked)
                }).SingleOrDefaultAsync();
        }
    }
}