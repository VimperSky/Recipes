using FluentValidation;
using Recipes.Application.DTOs.Recipe;

namespace Recipes.WebApi.DTOValidators.Recipe
{
    public class RecipeCreateDtoValidator: AbstractValidator<RecipeCreateDto>
    {
        public RecipeCreateDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}