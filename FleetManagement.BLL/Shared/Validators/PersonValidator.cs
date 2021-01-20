using FleetManagement.BLL.Shared.Validators.Interfaces;
using FleetManagement.Models;
using FluentValidation;

namespace FleetManagement.BLL.Shared.Validators
{
    public class PersonValidator : AbstractValidator<Person>
    {
        public PersonValidator(IBelgianNationalNumberValidator belgianNationalNumberValidator)
        {
            RuleFor(person => person.NationalNumber)
                .Must((person, nationalNumber) => belgianNationalNumberValidator
                .Validate(person.DateOfBirth, nationalNumber))
                .WithMessage("The supplied national number is not valid. Checksum failed, check given numbers and birth date.");

            RuleFor(person => person.NationalNumber)
                .Must((person, nationalNumber) => belgianNationalNumberValidator
                .ValidateFormat(nationalNumber))
                .WithMessage("The supplied national number is not valid. Formatting is incorrect.");

            RuleFor(person => person.FirstName)
                .MaximumLength(50)
                .NotEmpty();

            RuleFor(person => person.LastName)
                .MaximumLength(50)
                .NotEmpty();

            RuleFor(person => person.City)
                .MaximumLength(50)
                .NotEmpty();

            RuleFor(person => person.AddressLine)
                .MaximumLength(100)
                .NotEmpty();

            RuleFor(person => person.ZipCode)
                .InclusiveBetween(1000, 9999)
                .NotEmpty();
        }
    }
}
