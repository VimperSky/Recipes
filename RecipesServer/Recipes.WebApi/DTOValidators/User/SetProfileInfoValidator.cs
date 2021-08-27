using FluentValidation;
using Recipes.WebApi.DTOs.User;
using static Recipes.WebApi.DTOValidators.User.RegisterValidator;

namespace Recipes.WebApi.DTOValidators.User
{
    public class SetProfileInfoValidator : AbstractValidator<SetProfileInfo>
    {
        public const int MaxBioLength = 150;

        public SetProfileInfoValidator()
        {
            RuleFor(x => x.Login).NotEmpty().Length(MinLoginLength, MaxLoginLength)
                .Matches(LoginRegex);

            RuleFor(x => x.Name).NotEmpty().MaximumLength(MaxNameLength);

            RuleFor(x => x.Password).NotEmpty().Length(MinPasswordLength, MaxPasswordLength)
                .Matches(PasswordRegex);

            When(x => x.Bio != null, () => { RuleFor(x => x.Bio).MaximumLength(MaxBioLength); });
        }
    }
}