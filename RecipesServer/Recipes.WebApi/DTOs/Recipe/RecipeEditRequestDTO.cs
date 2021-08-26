namespace Recipes.WebApi.DTOs.Recipe
{
    public class RecipeEditRequestDTO : RecipeBaseDto
    {
        public int Id { get; set; }
        public IngredientDto[] Ingredients { get; init; }
        public string[] Steps { get; init; }
    }
}