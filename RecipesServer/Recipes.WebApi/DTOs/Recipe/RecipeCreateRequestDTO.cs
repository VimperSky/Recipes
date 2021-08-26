namespace Recipes.WebApi.DTOs.Recipe
{
    public class RecipeCreateRequestDTO : RecipeBaseDto
    {
        public IngredientDto[] Ingredients { get; init; }
        public string[] Steps { get; init; }
    }
}