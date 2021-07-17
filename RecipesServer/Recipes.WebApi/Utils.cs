namespace Recipes.WebApi
{
    public static class Utils
    {
        public static string GetRecipeImagePath(string localPath)
        {
            return "images/" + localPath;
        }
    }
}