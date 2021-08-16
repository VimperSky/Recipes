using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Recipes.Application.Exceptions;

namespace Recipes.Application.Services.Recipes
{
    public class ImageFileSaver : IImageFileSaver
    {
        public async Task<string> CreateFile(IFormFile formFile)
        {
            var guid = Guid.NewGuid();
            var targetImagePath = Path.Combine($"images/recipe_img_{guid}.{Path.GetExtension(formFile.FileName)}");
            var fullImagePath = Path.Combine("Storage", targetImagePath);

            try
            {
                await using Stream fileStream = new FileStream(fullImagePath, FileMode.Create);
                await formFile.CopyToAsync(fileStream);
            }
            catch (Exception e)
            {
                throw new ImageSavingException("Произошла ошибка при создании файла изображения", e);
            }

            return targetImagePath;
        }
    }
}