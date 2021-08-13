namespace Recipes.Application.DTOs.Recipe
{
    public abstract class RecipeBaseDto
    {
        public string Name { get; init; }
        public string Description { get; init; }
        public ushort? CookingTimeMin { get; init; }
        public ushort? Portions { get; init; }
    }
}