namespace Recipes.WebApi.DTOs.Recipe
{
    public abstract class RecipeBaseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ushort CookingTimeMin { get; set; }
        public ushort Portions { get; set; }
        public string[] Tags { get; set; }
    }
}