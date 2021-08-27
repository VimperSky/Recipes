using System.Threading.Tasks;
using Recipes.Domain.Models;

namespace Recipes.Domain.Repositories
{
    public interface IActivityRepository
    {
        Task<Activity> GetOrCreateActivity(int recipeId, int userId);

        Task<UserRecipesActivity> GetUserRecipesActivity(int userId, int[] recipeIds);

        Task<UserActivitySummary> GetUserActivitySummary(int userId);
    }
}