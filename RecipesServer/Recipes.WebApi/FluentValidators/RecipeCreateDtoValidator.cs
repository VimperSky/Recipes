using FluentValidation;
using Recipes.WebApi.DTO.Recipe;

namespace Recipes.WebApi.FluentValidators
{
    public class RecipeCreateDtoValidator: AbstractValidator<RecipeCreateDto>
    {
        public RecipeCreateDtoValidator()
        {
            RuleFor(x => x.ImagePath).Null();
        }
    }
}