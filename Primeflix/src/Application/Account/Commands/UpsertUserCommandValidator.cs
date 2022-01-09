using FluentValidation;
using Primeflix.Application.Common.Extensions;
using Primeflix.Application.Common.Validators;

namespace Primeflix.Application.Account.Commands;

public class UpsertUserCommandValidator : AbstractValidator<UpsertUserCommand>
{
    public UpsertUserCommandValidator()
    {
        RuleFor(v => v.FirstName)
            .NameValidation();

        RuleFor(v => v.LastName)
            .NameValidation();

        RuleFor(v => v.PhoneNumber)
            .PhoneNumberValidation();

        RuleFor(v => v.Address).SetValidator(new AddressDtoValidator());
    }
}