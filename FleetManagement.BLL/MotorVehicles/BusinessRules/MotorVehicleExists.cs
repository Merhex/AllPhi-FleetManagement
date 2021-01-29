using FleetManagement.BLL.MotorVehicles.Contracts;
using FleetManagement.DAL.Repositories.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.BLL
{
    public class MotorVehicleExists : IBusinessRule<IMotorVehicleContract>
    {
        private readonly IMotorVehicleRepository _motorVehicleRepository;

        public event BusinessRuleFailureEventHandler<IMotorVehicleContract> Failure;

        public MotorVehicleExists(IMotorVehicleRepository motorVehicleRepository)
        {
            _motorVehicleRepository = motorVehicleRepository;
        }

        public async Task Handle(IMotorVehicleContract contract, CancellationToken token = default)
        {
            var motorVehicle = await _motorVehicleRepository.FindByChassisNumberAsync(contract.ChassisNumber, token);

            if (motorVehicle is not null)
            {
                OnFailure(new BusinessRuleFailureEventArgs
                {
                    Messages = { $"The motorvehicle with chassisnumber: {contract.ChassisNumber} already exists." }
                });
            }
        }

        protected virtual void OnFailure(BusinessRuleFailureEventArgs args)
        {
            Failure?.Invoke(this, args);
        }
    }
}
