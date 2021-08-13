using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Recipes.Application.Services.Recipes
{
    public interface IImageFileSaver
    {
        Task<string> CreateFile(IFormFile formFile);
    }
}