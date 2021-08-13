using System;

namespace Recipes.Application.Exceptions
{
    public class ResourceNotFoundException: Exception
    {
        public string Value { get; }
        public ResourceNotFoundException(string message): base("ResourceNotFoundException: " + message)
        {
            Value = message;
        }
        
        public const string RecipeNotFound = "Такой рецепт не существует";
        public const string RecipesPageNotFound = "Такая страница с рецептами не существует";

    }
}