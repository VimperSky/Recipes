namespace Recipes.Application.Models.Recipe
{
    public class RecipeCreateCommand : RecipeBase
    {
        public Ingredient[] Ingredients { get; init; }
        public string[] Steps { get; init; }
    }
}