using FleetManagement.BLL.Commands;
using FleetManagement.Models;
using FluentValidation;

namespace FleetManagement.BLL.Validators
{
    public class CreateMotorVehicleCommandValidator : AbstractValidator<CreateMotorVehicleCommand>
    {
        public CreateMotorVehicleCommandValidator()
        {
            RuleFor(m => m.ChassisNumber)
                .Matches("[A-HJ-NPR-Z0-9]{17}")
                .WithMessage("The given VIN number is incorrect. Please check the formatting.");

            RuleFor(command => command.BodyType)
                .IsInEnum()
                .WithMessage("The given body type is not a valid option out of the option list.");

            RuleFor(command => command.PropulsionType)
                .IsInEnum()
                .WithMessage("The given propulsion type is not a valid option out of the option list.");
        }
    }
}
