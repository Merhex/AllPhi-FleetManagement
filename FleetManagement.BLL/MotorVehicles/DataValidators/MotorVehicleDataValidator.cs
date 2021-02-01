using FleetManagement.Models;
using FluentValidation;

namespace FleetManagement.BLL.MotorVehicles.Validators
{
    public class MotorVehicleDataValidator : AbstractValidator<MotorVehicle>
    {
        public MotorVehicleDataValidator()
        {
            RuleFor(motorVehicle => motorVehicle.ChassisNumber)
                .Matches("[A-HJ-NPR-Z0-9]{17}")
                .WithMessage("Please check the VIN formatting. Has to have 17 characters, not containing letters O, I and Q.")
                .NotEmpty();

            RuleFor(motorVehicle => motorVehicle.BodyType)
                .IsInEnum()
                .NotEmpty()
                .WithMessage("The given body type is not a valid option out of the option list.");

            RuleFor(motorVehicle => motorVehicle.PropulsionType)
                .IsInEnum()
                .NotEmpty()
                .WithMessage("The given propulsion type is not a valid option out of the option list.");

            RuleFor(motorVehicle => motorVehicle.Brand)
                .MaximumLength(50)
                .NotEmpty();

            RuleFor(motorVehicle => motorVehicle.Model)
                .MaximumLength(50)
                .NotEmpty();
        }
    }
}
