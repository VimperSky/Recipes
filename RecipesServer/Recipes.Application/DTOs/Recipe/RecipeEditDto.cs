namespace Recipes.Application.DTOs.Recipe
{
    public class RecipeEditDto: RecipeBaseDto
    {
        public int Id { get; init; }
        public IngredientDto[] Ingredients { get; init; }
        public string[] Steps { get; init; }
    }
}