using FluentValidation;
using Recipes.Application.DTOs.Recipe;

namespace Recipes.WebApi.DTOValidators.Recipe
{
    public class RecipeEditValidator : AbstractValidator<RecipeEditDto>
    {
        public RecipeEditValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}