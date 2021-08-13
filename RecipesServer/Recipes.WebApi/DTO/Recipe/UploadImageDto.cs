using Microsoft.AspNetCore.Http;

namespace Recipes.WebApi.DTO.Recipe
{
    public class UploadImageDto
    {
        public int RecipeId { get; init; }
        public IFormFile File { get; init; }
    }
}