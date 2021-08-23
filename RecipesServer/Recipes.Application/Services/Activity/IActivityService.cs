using System.Threading.Tasks;
using Recipes.Application.Permissions.Models;
using Recipes.Domain.Models;

namespace Recipes.Application.Services.Activity
{
    public interface IActivityService
    {
        Task SetActivity(int recipeId, UserClaims userClaims, ActivityType activityType);
        Task TakeActivity(int recipeId, UserClaims userClaims, ActivityType activityType);

        Task<UserActivity> GetUserActivity(UserClaims userClaims);
    }
}