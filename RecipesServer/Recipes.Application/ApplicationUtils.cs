namespace Recipes.Application
{
    public static class ApplicationUtils
    {
        public static string GetRecipeImagePath(string localPath)
        {
            return "images/" + localPath;
        }
    }
}