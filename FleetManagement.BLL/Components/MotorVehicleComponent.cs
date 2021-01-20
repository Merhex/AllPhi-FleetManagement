using FleetManagement.BLL.Commands;
using FleetManagement.BLL.Commands.Response;
using FleetManagement.BLL.Components.Interfaces;
using FleetManagement.BLL.Validators;
using FleetManagement.DAL.Repositories.Interfaces;
using FleetManagement.Models;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.BLL.Components
{
    public partial class MotorVehicleComponent : IMotorVehicleComponent
    {
        private readonly IMotorVehicleRepository _motorVehicleRepository;
        private readonly MotorVehicleValidator _motorVehicleValidator;

        public MotorVehicleComponent(
            IMotorVehicleRepository motorVehicleRepository,
            MotorVehicleValidator motorVehicleValidator)
        {
            _motorVehicleRepository = motorVehicleRepository;
            _motorVehicleValidator = motorVehicleValidator;
        }

        public async Task<ICommandResponse> CreateMotorVehicle(CreateMotorVehicleCommand command, CancellationToken token)
        {
            var match = await _motorVehicleRepository.FindByChassisNumber(command.ChassisNumber);

            if (match is not null) return CommandResponse.BadRequest("The given vehicle already exists");

            var motorVehicle = new MotorVehicle
            {
                 Brand = command.Brand,
                 Model = command.Model,
                 BodyType = command.BodyType,
                 ChassisNumber = command.ChassisNumber,
                 Operational = command.Operational,
                 PropulsionType = command.PropulsionType
            };

            var validation = await _motorVehicleValidator.ValidateAsync(motorVehicle, token);
            if (validation.IsValid is not true)
                return CommandResponse.BadRequest(validation);

            _motorVehicleRepository.Add(motorVehicle);

            var saved = await _motorVehicleRepository.SaveAsync();
            if (saved is not true)
                return CommandResponse.BadRequest("Something went wrong saving to the database.");


            return CommandResponse.Created();
        }
    }
}
