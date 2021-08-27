using System.Linq;
using FluentValidation;
using Recipes.WebApi.DTOs.Recipe;

namespace Recipes.WebApi.DTOValidators.Recipe
{
    public class UploadImageValidator : AbstractValidator<UploadImageRequestDTO>
    {
        private static readonly string[] AcceptedImageExtensions = { "image/jpeg", "image/png" };

        public UploadImageValidator()
        {
            RuleFor(x => x.RecipeId).NotEmpty().GreaterThan(0);
            RuleFor(x => x.File).NotNull().DependentRules(() =>
            {
                RuleFor(x => x.File.ContentType).Must(x => AcceptedImageExtensions.Contains(x));
            });
        }
    }
}