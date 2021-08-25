using System.Threading.Tasks;
using Recipes.Application.Permissions.Models;
using Recipes.Domain;
using Recipes.Domain.Models;
using Recipes.Domain.Repositories;

namespace Recipes.Application.Services.Activity
{
    public class ActivityService : IActivityService
    {
        private readonly IActivityRepository _activityRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ActivityService(IActivityRepository activityRepository, IUnitOfWork unitOfWork)
        {
            _activityRepository = activityRepository;
            _unitOfWork = unitOfWork;
        }
        
        public async Task AddActivity(int recipeId, UserClaims userClaims, ActivityType activityType)
        {
            await ProcessActivity(recipeId, userClaims.UserId, activityType, true);
        }

        public async Task RemoveActivity(int recipeId, UserClaims userClaims, ActivityType activityType)
        {
            await ProcessActivity(recipeId, userClaims.UserId, activityType, false);
        }

        public async Task<UserActivity> GetUserActivity(UserClaims userClaims)
        {
            return await _activityRepository.GetUserActivity(userClaims.UserId);
        }

        public async Task<UserActivitySummary> GetUserActivitySummary(UserClaims userClaims)
        {
            return await _activityRepository.GetUserActivitySummary(userClaims.UserId);
        }

        private async Task ProcessActivity(int recipeId, int userId, ActivityType activityType, bool action)
        {
            var userActivity = await _activityRepository.GetOrCreateActivity(recipeId, userId);
            switch (activityType)
            {
                case ActivityType.Like:
                    userActivity.IsLiked = action;
                    break;
                case ActivityType.Star:
                    userActivity.IsStarred = action;
                    break;
            }
            
            _unitOfWork.Commit();
        }

    }
}