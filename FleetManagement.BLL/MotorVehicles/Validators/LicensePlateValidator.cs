using FleetManagement.Models;
using FluentValidation;

namespace FleetManagement.BLL.MotorVehicles.Validators
{
    public class LicensePlateValidator : AbstractValidator<LicensePlate>
    {
        public LicensePlateValidator()
        {
            RuleFor(licensePlate => licensePlate.Identifier)
                .MaximumLength(9)
                .NotEmpty();
        }
    }
}
