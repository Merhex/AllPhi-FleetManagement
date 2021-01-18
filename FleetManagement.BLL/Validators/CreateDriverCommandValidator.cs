using FleetManagement.BLL.Commands;
using FleetManagement.BLL.Validators.Interfaces;
using FleetManagement.DAL.Repositories.Interfaces;
using FluentValidation;
using System;

namespace FleetManagement.BLL.Validators
{
    public class CreateDriverCommandValidator : AbstractValidator<CreateDriverCommand>
    {
        public CreateDriverCommandValidator(IBelgianNationalNumberValidator belgianNationalNumberValidator)
        {
            RuleFor(command => command.NationalNumber)
                .Must((driver, nationalNumber) => belgianNationalNumberValidator
                .ValidateChecksum(driver.DateOfBirth, nationalNumber))
                .WithMessage("The supplied national number is not valid. Checksum failed, check given numbers and birth date.");

            RuleFor(command => command.NationalNumber)
                .Must((driver, nationalNumber) => belgianNationalNumberValidator
                .ValidateFormat(nationalNumber))
                .WithMessage("The supplied national number is not valid. Formatting is incorrect.");
        }
    }
}
