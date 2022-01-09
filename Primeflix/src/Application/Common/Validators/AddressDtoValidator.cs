using FluentValidation;
using Primeflix.Application.Common.Models;

namespace Primeflix.Application.Common.Validators;

public class AddressDtoValidator : AbstractValidator<AddressDto>
{
    public AddressDtoValidator()
    {
        RuleFor(v => v.Country)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(v => v.City)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(v => v.PostalCode)
            .NotEmpty()
            .Length(2, 7);

        RuleFor(v => v.Street)
            .NotEmpty()
            .Length(5, 100);

        RuleFor(v => v.Number)
            .NotEmpty()
            .MaximumLength(3);

        RuleFor(v => v.POBox)
            .Length(0, 3);
    }
}