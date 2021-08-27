namespace Recipes.Application.Models.Recipe
{
    public class RecipeEditCommand : RecipeBase
    {
        public int Id { get; set; }
        public Ingredient[] Ingredients { get; init; }
        public string[] Steps { get; init; }
    }
}