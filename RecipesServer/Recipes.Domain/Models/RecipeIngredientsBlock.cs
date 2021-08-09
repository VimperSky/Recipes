namespace Recipes.Domain.Models
{
    public class RecipeIngredientsBlock
    {
        public int Id { get; init; }
        public int Position { get; init; }
        public string Header { get; init; }
        public string Value { get; init; }
    }
}