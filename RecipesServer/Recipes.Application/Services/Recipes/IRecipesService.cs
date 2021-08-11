using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Recipes.Application.DTOs.Recipe;
using Recipes.Application.Permissions.Models;

namespace Recipes.Application.Services.Recipes
{
    public interface IRecipesService
    {
        Task<RecipesPageDto> GetRecipesPage(string searchString, int pageSize, int page);
        Task<RecipeDetailDto> GetRecipeDetail(int id);
        Task<int> CreateRecipe(RecipeCreateDto recipeCreateDto, UserClaims userClaims);
        Task EditRecipe(RecipeEditDto recipeEditDto, UserClaims userClaims);
        Task UploadImage(int recipeId, IFormFile formFile, UserClaims userClaims);
        Task DeleteRecipe(int recipeId, UserClaims userClaims);
    }
}