namespace Recipes.Application.Exceptions
{
    public class ElementNotFoundException: ExceptionWithValue
    {
        public ElementNotFoundException(string message): base(message) {}

        public const string RecipeNotFound = "Такой рецепт не существует";
        public const string RecipesPageNotFound = "Такая страница с рецептами не существует";
        public const string AccountDoesNotExist = "Такой аккаунт не существует.";
    }
}