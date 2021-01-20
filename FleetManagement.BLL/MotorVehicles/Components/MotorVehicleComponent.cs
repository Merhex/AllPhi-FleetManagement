using FleetManagement.BLL.MotorVehicles.Commands;
using FleetManagement.BLL.MotorVehicles.Components.Interfaces;
using FleetManagement.BLL.MotorVehicles.Validators;
using FleetManagement.BLL.Shared;
using FleetManagement.BLL.Shared.Interfaces;
using FleetManagement.DAL.Repositories.Interfaces;
using FleetManagement.Models;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.BLL.MotorVehicles.Components
{
    public partial class MotorVehicleComponent : IMotorVehicleComponent
    {
        private readonly IMotorVehicleRepository _motorVehicleRepository;
        private readonly ILicensePlateRepository _licensePlateRepository;
        private readonly LicensePlateValidator _licensePlateValidator;
        private readonly MotorVehicleValidator _motorVehicleValidator;

        public MotorVehicleComponent(
            IMotorVehicleRepository motorVehicleRepository,
            ILicensePlateRepository licensePlateRepository,
            LicensePlateValidator licensePlateValidator,
            MotorVehicleValidator motorVehicleValidator)
        {
            _motorVehicleRepository = motorVehicleRepository;
            _licensePlateRepository = licensePlateRepository;
            _licensePlateValidator = licensePlateValidator;
            _motorVehicleValidator = motorVehicleValidator;
        }

        public async Task<ICommandResponse> ChangeOperationalStatusAsync(ChangeMotorVehicleOperationalStatusCommand command, CancellationToken cancellationToken)
        {
            var motorVehicle = await _motorVehicleRepository.FindByIdAsync(command.MotorVehicleId, cancellationToken);

            if (motorVehicle is null)
                return CommandResponse.BadRequest("Could not find a motor vehicle with given id.");

            motorVehicle.Operational = command.Operational;

            var saved = await _motorVehicleRepository.SaveAsync();
            if (saved is not true)
                return CommandResponse.BadRequest("Something went wrong saving to the database.");


            return CommandResponse.Ok();
        }

        public async Task<ICommandResponse> CreateLicensePlateAsync(CreateLicensePlateCommand command, CancellationToken cancellationToken)
        {
            var licensePlate = await _licensePlateRepository.FindByIdentifierAsync(command.Identifier, cancellationToken);

            if (licensePlate is not null)
                return CommandResponse.BadRequest("The license plate with given identifier already exists.");

            licensePlate = new LicensePlate
            {
                Identifier = command.Identifier,
                InUse = command.InUse
            };

            var validation = await _licensePlateValidator.ValidateAsync(licensePlate, cancellationToken);
            if (validation.IsValid is not true)
                return CommandResponse.BadRequest(validation);

            _licensePlateRepository.Add(licensePlate);

            var saved = await _licensePlateRepository.SaveAsync();
            if (saved is not true)
                return CommandResponse.BadRequest("Something went wrong saving to the database.");


            return CommandResponse.Created();
        }

        public async Task<ICommandResponse> CreateMotorVehicleAsync(CreateMotorVehicleCommand command, CancellationToken cancellationToken)
        {
            var motorVehicle = await _motorVehicleRepository.FindByChassisNumber(command.ChassisNumber, cancellationToken);

            if (motorVehicle is not null) return CommandResponse.BadRequest("The given vehicle already exists");

            motorVehicle = new MotorVehicle
            {
                 Brand = command.Brand,
                 Model = command.Model,
                 BodyType = command.BodyType,
                 ChassisNumber = command.ChassisNumber,
                 Operational = command.Operational,
                 PropulsionType = command.PropulsionType
            };

            var validation = await _motorVehicleValidator.ValidateAsync(motorVehicle, cancellationToken);
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
