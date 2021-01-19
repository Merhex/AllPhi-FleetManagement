using FleetManagement.Models;
using FluentValidation;
using System;

namespace FleetManagement.BLL.Validators
{
    public class FuelCardValidator : AbstractValidator<FuelCard>
    {
        public FuelCardValidator()
        {
            RuleFor(card => card.AuthenticationType)
                .IsInEnum();

            RuleFor(card => card.CardNumber)
                .MaximumLength(20)
                .Matches("^[0-9]*$")
                .WithMessage("The card numbers can only contain numbers. With a maximum length of 20 characters")
                .NotEmpty();

            RuleFor(card => card.ExpiryDate)
                .GreaterThanOrEqualTo(DateTime.Now)
                .WithMessage("The card is expired. The system will not allow expired cards.");

            RuleFor(card => card.PinCode)
                .InclusiveBetween(0, 999999);

            RuleFor(card => card.PropulsionTypes)
                .IsInEnum()
                .WithMessage("The given propulsion type is not a valid option out of the option list.");
        }
    }
}
