using System;

namespace Recipes.Application.Exceptions
{
    public class ResourceNotFoundException: ExceptionWithValue
    {
        public ResourceNotFoundException(string message): base(message) {}

        public const string RecipeNotFound = "Такой рецепт не существует";
        public const string RecipesPageNotFound = "Такая страница с рецептами не существует";

    }
}