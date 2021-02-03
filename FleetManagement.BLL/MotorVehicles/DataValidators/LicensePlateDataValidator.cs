using FleetManagement.Models;
using FluentValidation;

namespace FleetManagement.BLL.MotorVehicles.Validators
{
    public class LicensePlateDataValidator : AbstractValidator<LicensePlate>
    {
        public LicensePlateDataValidator()
        {
            RuleFor(licensePlate => licensePlate.Identifier)
                .MaximumLength(9)
                .NotEmpty()
                .Matches("[A-Z-0-9.]{1,9}")
                .WithMessage("The given license plate identifier is not a valid belgian license plate identifier.");
        }
    }
}
