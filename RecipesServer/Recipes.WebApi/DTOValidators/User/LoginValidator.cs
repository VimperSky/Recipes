using FluentValidation;
using Recipes.WebApi.DTO.User;
using static Recipes.WebApi.DTOValidators.User.RegisterValidator;

namespace Recipes.WebApi.DTOValidators.User
{
    public class LoginValidator : AbstractValidator<LoginDto>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Login).NotEmpty().Length(MinLoginLength, MaxLoginLength)
                .Matches(LoginRegex);

            RuleFor(x => x.Password).NotEmpty().Length(MinPasswordLength, MaxPasswordLength)
                .Matches(PasswordRegex);
        }
    }
}