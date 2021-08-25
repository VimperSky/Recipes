﻿using System.Threading.Tasks;
using Recipes.Application.Permissions.Models;
using Recipes.Domain.Models;

namespace Recipes.Application.Services.Activity
{
    public interface IActivityService
    {
        Task AddActivity(int recipeId, UserClaims userClaims, ActivityType activityType);
        Task RemoveActivity(int recipeId, UserClaims userClaims, ActivityType activityType);
        Task<UserActivity> GetUserActivity(UserClaims userClaims);

        Task<UserActivityOverview> GetUserActivityOverview(UserClaims userClaims);
    }
}