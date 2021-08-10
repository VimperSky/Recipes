using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Recipes.Application.Services.Recipes
{
    public static class FileTools
    {
        public static async Task<string> CreateFile(IFormFile formFile)
        {
            var guid = Guid.NewGuid();
            var imagePath = Path.Combine("Storage", "images", $"recipe_img_{guid}.{Path.GetExtension(formFile.FileName)}");
            await using Stream fileStream = new FileStream(imagePath, FileMode.Create);
            await formFile.CopyToAsync(fileStream);

            return Path.Combine($"images/recipe_img_{guid}.{Path.GetExtension(formFile.FileName)}");
        }
    }
}