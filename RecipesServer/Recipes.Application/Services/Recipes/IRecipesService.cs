﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Recipes.Application.DTOs.Recipe;
using Recipes.Application.Exceptions;
using Recipes.Application.Permissions.Models;
using Recipes.Domain.Models;

namespace Recipes.Application.Services.Recipes
{
    public interface IRecipesService
    {
        /// <summary>
        ///     Get page of recipes
        /// </summary>
        /// <exception cref="ElementNotFoundException"></exception>
        Task<RecipesPageDto> GetRecipesPage(int pageSize, int page,
            RecipesType recipesType, UserClaims authorClaims, string searchString = null);

        /// <summary>
        ///     Get detail of recipe with the given id
        /// </summary>
        /// <returns></returns>
        Task<RecipeDetailDto> GetRecipeDetail(int id, UserClaims authorClaims);

        /// <summary>
        ///     Create a new recipe
        /// </summary>
        /// <param name="recipeCreateDto"></param>
        /// <param name="userClaims"></param>
        Task<int> CreateRecipe(RecipeCreateDto recipeCreateDto, UserClaims userClaims);

        /// <summary>
        ///     Edit recipe
        /// </summary>
        /// <param name="recipeEditDto"></param>
        /// <param name="userClaims"></param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="PermissionException"></exception>
        /// <exception cref="ElementNotFoundException"></exception>
        Task EditRecipe(RecipeEditDto recipeEditDto, UserClaims userClaims);

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

        Task<RecipePreviewDto> GetRecipeOfTheDay();

        Task<int> GetAuthorRecipesCount(UserClaims userClaims);
    }
}