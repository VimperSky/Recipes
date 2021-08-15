using System.Linq;
using FluentValidation;
using Recipes.WebApi.DTO.Recipe;

namespace Recipes.WebApi.DTOValidators.Recipe
{
    public class UploadImageDtoValidator: AbstractValidator<UploadImageDto>
    {
        private static readonly string[] AcceptedImageExtensions = {"image/jpeg", "image/png" };

        public UploadImageDtoValidator()
        {
            RuleFor(x => x.RecipeId).NotEmpty().GreaterThan(0);
            RuleFor(x => x.File).NotNull().DependentRules(() =>
            {
                RuleFor(x => x.File.ContentType).Must(x => AcceptedImageExtensions.Contains(x));
            });
        }
    }
}