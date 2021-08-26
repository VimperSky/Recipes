using System.Threading.Tasks;
using Recipes.Application.Permissions.Models;
using Recipes.Domain.Models;

namespace Recipes.Application.Services.Activity
{
    public interface IActivityService
    {
        Task AddActivity(int recipeId, UserClaims userClaims, ActivityType activityType);
        Task RemoveActivity(int recipeId, UserClaims userClaims, ActivityType activityType);
        Task<UserRecipesActivityResult> GetUserRecipesActivity(int[] recipesIds, UserClaims userClaims);
        Task<UserActivitySummary> GetUserActivitySummary(UserClaims userClaims);
    }
}