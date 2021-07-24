namespace Recipes.WebApi.DTO.Recipe
{
    public class RecipeDetail: RecipeBase
    {
        public int Id { get; init; }
        public Ingredient[] Ingredients { get; init; }
        public string[] Steps { get; init; }
    }
}