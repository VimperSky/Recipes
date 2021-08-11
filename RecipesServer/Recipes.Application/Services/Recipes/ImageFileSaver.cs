using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Recipes.Application.Services.Recipes
{
    public class ImageFileSaver : IImageFileSaver
    {
        public async Task<string> CreateFile(IFormFile formFile)
        {
            var guid = Guid.NewGuid();
            var targetImagePath = Path.Combine($"images/recipe_img_{guid}.{Path.GetExtension(formFile.FileName)}");
            
            var fullImagePath = Path.Combine("Storage", targetImagePath);
            await using Stream fileStream = new FileStream(fullImagePath, FileMode.Create);
            await formFile.CopyToAsync(fileStream);

            return targetImagePath;
        }
    }
}