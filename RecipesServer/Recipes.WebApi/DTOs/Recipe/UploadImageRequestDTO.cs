using Microsoft.AspNetCore.Http;

namespace Recipes.WebApi.DTOs.Recipe
{
    public class UploadImageRequestDTO
    {
        public int RecipeId { get; init; }
        public IFormFile File { get; init; }
    }
}