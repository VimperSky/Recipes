using FluentValidation;
using Recipes.WebApi.DTOs.Recipe;

namespace Recipes.WebApi.DTOValidators.Recipe
{
    public class RecipeEditValidator : AbstractValidator<RecipeEditRequestDTO>
    {
        public RecipeEditValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}