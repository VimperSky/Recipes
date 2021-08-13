using System;

namespace Recipes.Application.Exceptions
{
    public class ImageSavingException: Exception
    {
        public ImageSavingException(string message, Exception inner): base($"ImageSavingException: {message}", inner)
        {
        }
    }
}