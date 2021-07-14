namespace Recipes.WebApi.DTO.Recipe
{
    public abstract class RecipeBase
    {
        public string Name { get; init; }
        public string Description { get; init; }
        public ushort CookingTime { get; init; }
        public ushort Portions { get; init; }
        public string ImagePath { get; init; }
    }
}