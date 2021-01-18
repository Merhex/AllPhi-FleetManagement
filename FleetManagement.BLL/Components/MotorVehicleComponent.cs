using FleetManagement.BLL.Commands;
using FleetManagement.BLL.Components.Interfaces;
using FleetManagement.DAL.Repositories.Interfaces;
using FleetManagement.Models;
using System.Threading.Tasks;

namespace FleetManagement.BLL.Components
{
    public class MotorVehicleComponent : IMotorVehicleComponent
    {
        private readonly IMotorVehicleRepository _motorVehicleRepository;

        public MotorVehicleComponent(IMotorVehicleRepository motorVehicleRepository)
        {
            _motorVehicleRepository = motorVehicleRepository;
        }

        public async Task CreateMotorVehicle(CreateMotorVehicleCommand command)
        {
            var match = await _motorVehicleRepository.FindByChassisNumber(command.ChassisNumber);

            if (match is not null) return;

            var motorVehicle = new MotorVehicle
            {
                 BodyType = command.BodyType,
                 ChassisNumber = command.ChassisNumber,
                 Operational = command.Operational,
                 PropulsionType = command.PropulsionType
            };

            _motorVehicleRepository.Add(motorVehicle);
            await _motorVehicleRepository.SaveAsync();
        }
    }
}
