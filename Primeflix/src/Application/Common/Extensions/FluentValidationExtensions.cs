using FluentValidation;

namespace Primeflix.Application.Common.Extensions;

public static class FluentValidationExtensions
{
    public static IRuleBuilderOptions<T, string> NameValidation<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");
    }

    public static IRuleBuilderOptions<T, string> PhoneNumberValidation<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .Length(6, 14).WithMessage("{PropertyName} must be between 6 and 14 caracters")
            .Matches("^[0-9]*$").WithMessage("{PropertyName should only have numbers");
    }
}