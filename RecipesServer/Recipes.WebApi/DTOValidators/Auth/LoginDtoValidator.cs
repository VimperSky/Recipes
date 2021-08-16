using FluentValidation;
using Recipes.WebApi.DTO.User;
using static Recipes.WebApi.DTOValidators.Auth.RegisterDtoValidator;

namespace Recipes.WebApi.DTOValidators.Auth
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