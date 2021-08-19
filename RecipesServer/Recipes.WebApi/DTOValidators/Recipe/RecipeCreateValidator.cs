using FluentValidation;
using Recipes.Application.DTOs.Recipe;

namespace Recipes.WebApi.DTOValidators.Recipe
{
    public class RecipeCreateValidator : AbstractValidator<RecipeCreateDto>
    {
        public RecipeCreateValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}