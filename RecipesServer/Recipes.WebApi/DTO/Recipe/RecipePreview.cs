namespace Recipes.WebApi.DTO.Recipe
{
    public class RecipePreview: RecipeBase
    {        
        public int Id { get; init; }

        public static RecipePreview FromModel(Domain.Models.Recipe recipe)
        {
            return new()
            {
                Id = recipe.Id, Description = recipe.Description, Name = recipe.Name, Portions = recipe.Portions,
                CookingTime = recipe.CookingTimeMin, ImagePath = Utils.GetRecipeImagePath(recipe.ImagePath)
            };
        }
    }
}