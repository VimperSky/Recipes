using FluentValidation;
using Recipes.WebApi.DTO.User;

namespace Recipes.WebApi.DTOValidators.User
{
    public class RegisterValidator : AbstractValidator<RegisterDto>
    {
        public const int MinLoginLength = 3;
        public const int MaxLoginLength = 20;

        public const int MinPasswordLength = 8;
        public const int MaxPasswordLength = 24;

        public const int MaxNameLength = 20;

        public const string LoginRegex = @"^[A-Za-z0-9_]+$";
        public const string PasswordRegex = @"^[\S]+$";

        public RegisterValidator()
        {
            RuleFor(x => x.Login).NotEmpty().Length(MinLoginLength, MaxLoginLength)
                .Matches(LoginRegex);

            RuleFor(x => x.Name).NotEmpty().MaximumLength(MaxNameLength);

            RuleFor(x => x.Password).NotEmpty().Length(MinPasswordLength, MaxPasswordLength)
                .Matches(PasswordRegex);
        }
    }
}