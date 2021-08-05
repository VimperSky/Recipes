namespace Recipes.WebApi.DTO.Recipe
{
    public class RecipeDetailDto: RecipeBaseDto
    {
        public int Id { get; init; }
        public IngredientDto[] Ingredients { get; init; }
        public string[] Steps { get; init; }
    }
}