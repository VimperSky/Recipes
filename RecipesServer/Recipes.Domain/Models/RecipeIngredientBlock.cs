namespace Recipes.Domain.Models
{
    public class RecipeIngredientBlock
    {
        public int Id { get; init; }
        public int Position { get; init; }
        public string Header { get; init; }
        public string Text { get; init; }
    }
}