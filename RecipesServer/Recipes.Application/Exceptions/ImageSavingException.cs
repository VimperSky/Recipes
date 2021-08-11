using System;

namespace Recipes.Application.Exceptions
{
    public class ImageSavingException: Exception
    {
        public ImageSavingException(string message, Exception inner): base($"ImageSavingException: {message}", inner)
        {
        }
        
        public const string NotEnoughPermissionsToModifyResource = "У вас нет прав на модификацию данного ресурса.";
    }
}