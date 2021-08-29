using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Recipes.Application.Exceptions;
using Recipes.Application.Models.Recipe;
using Recipes.Application.Permissions.Models;

namespace Recipes.Application.Services.Recipes
{
    public interface IRecipesService
    {
        /// <summary>
        ///     Get page of recipes
        /// </summary>
        /// <exception cref="ElementNotFoundException"></exception>
        Task<RecipesPageResult> GetRecipesPage(int pageSize, int page,
            RecipesSelectionType recipesSelectionType, UserClaims userClaims, string searchString = null);

        /// <summary>
        ///     Get detail of recipe with the given id
        /// </summary>
        /// <returns></returns>
        Task<RecipeDetailResult> GetRecipeDetail(int id, UserClaims userClaims);

        /// <summary>
        ///     Create a new recipe
        /// </summary>
        /// <param name="recipeCreateCommandDto"></param>
        /// <param name="userClaims"></param>
        Task<int> CreateRecipe(RecipeCreateCommand recipeCreateCommandDto, UserClaims userClaims);

        /// <summary>
        ///     Edit recipe
        /// </summary>
        /// <param name="recipeEditCommandDto"></param>
        /// <param name="userClaims"></param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="PermissionException"></exception>
        /// <exception cref="ElementNotFoundException"></exception>
        Task EditRecipe(RecipeEditCommand recipeEditCommandDto, UserClaims userClaims);

        /// <summary>
        ///     Upload file for recipe with the given id
        /// </summary>
        /// <param name="recipeId"></param>
        /// <param name="formFile"></param>
        /// <param name="userClaims"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ElementNotFoundException"></exception>
        /// <exception cref="PermissionException"></exception>
        /// <exception cref="ImageSavingException"></exception>
        Task UploadImage(int recipeId, IFormFile formFile, UserClaims userClaims);

        /// <summary>
        ///     Delete recipe with the given id
        /// </summary>
        /// <param name="recipeId"></param>
        /// <param name="userClaims"></param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="PermissionException"></exception>
        /// <exception cref="ElementNotFoundException"></exception>
        Task DeleteRecipe(int recipeId, UserClaims userClaims);

        
        /// <summary>
        ///     Returns current recipe of the day 
        /// </summary>
        /// <returns></returns>
        Task<RecipePreviewResult> GetRecipeOfTheDay();

        /// <summary>
        ///     Get recipes count of target Author
        /// </summary>
        /// <param name="userClaims"></param>
        /// <returns></returns>
        Task<int> GetAuthorRecipesCount(UserClaims userClaims);
    }
}