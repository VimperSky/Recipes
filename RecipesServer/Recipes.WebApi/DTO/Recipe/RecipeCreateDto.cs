namespace Recipes.WebApi.DTO.Recipe
{
    public class RecipeCreateDto: RecipeBaseDto
    {
        public IngredientDto[] Ingredients { get; init; }
        public string[] Steps { get; init; }
    }
}