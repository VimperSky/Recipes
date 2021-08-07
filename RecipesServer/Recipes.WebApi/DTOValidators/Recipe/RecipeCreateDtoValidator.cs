using System.Linq;
using FluentValidation;
using Recipes.Application.DTOs.Recipe;

namespace Recipes.WebApi.DTOValidators.Recipe
{
    public class RecipeCreateDtoValidator: AbstractValidator<RecipeCreateDto>
    {
        private static readonly string[] AcceptedImageExtensions = {"image/jpeg", "image/png" };
        public RecipeCreateDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.ImageFile.ContentType).Must(x => AcceptedImageExtensions.Contains(x));
        }
    }
}