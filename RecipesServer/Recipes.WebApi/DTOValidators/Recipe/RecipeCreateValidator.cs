using FluentValidation;
using Recipes.WebApi.DTOs.Recipe;

namespace Recipes.WebApi.DTOValidators.Recipe
{
    public class RecipeCreateValidator : AbstractValidator<RecipeCreateRequestDTO>
    {
        public RecipeCreateValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}