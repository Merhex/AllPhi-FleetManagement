using FleetManagement.BLL.MotorVehicles.Contracts;
using FleetManagement.DAL.Repositories.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.BLL
{
    public class MotorVehicleOperationalStatusMustBeDifferent : IBusinessRule<IChangeMotorVehicleOperationalStatusContract>
    {
        private readonly IMotorVehicleRepository _motorVehicleRepository;

        public event BusinessRuleFailureEventHandler<IChangeMotorVehicleOperationalStatusContract> Failure;

        public MotorVehicleOperationalStatusMustBeDifferent(IMotorVehicleRepository motorVehicleRepository)
        {
            _motorVehicleRepository = motorVehicleRepository;
        }

        public async Task Handle(IChangeMotorVehicleOperationalStatusContract contract, CancellationToken token = default)
        {
            var motorVehicle = await _motorVehicleRepository.FindByChassisNumberAsync(contract.ChassisNumber, token);

            if (motorVehicle is not null)
                if (contract.Operational.Equals(motorVehicle.Operational))
                    OnFailure(new BusinessRuleFailureEventArgs
                    {
                        Messages = { "The operational status of the vehicle is already the same value." }
                    });
        }

        protected virtual void OnFailure(BusinessRuleFailureEventArgs args)
        {
            Failure?.Invoke(this, args);
        }
    }
}
