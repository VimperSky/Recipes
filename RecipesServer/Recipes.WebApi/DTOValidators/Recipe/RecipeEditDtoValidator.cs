using FluentValidation;
using Recipes.Application.DTOs.Recipe;

namespace Recipes.WebApi.DTOValidators.Recipe
{
    public class RecipeEditDtoValidator: AbstractValidator<RecipeEditDto>
    {
        public RecipeEditDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}