using FluentValidation;
using Recipes.WebApi.DTO.Auth;
using static Recipes.WebApi.FluentValidators.RegisterDtoValidator;

namespace Recipes.WebApi.FluentValidators
{
    public class LoginDtoValidator: AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(x => x.Login).NotEmpty().Length(MinLoginLength, MaxLoginLength)
                .Matches(LoginRegex);
            
            RuleFor(x => x.Password).NotEmpty().Length(MinPasswordLength, MaxPasswordLength)
                .Matches(PasswordRegex);
        }
    }
}