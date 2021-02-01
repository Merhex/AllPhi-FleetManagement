using FleetManagement.DAL.Repositories.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.BLL
{
    public class CheckMotorVehicleExists : IBusinessRule
    {
        private readonly IMotorVehicleRepository _motorVehicleRepository;
        private readonly string _chassisNumber;

        public CheckMotorVehicleExists(IMotorVehicleRepository motorVehicleRepository, string chassisNumber)
        {
            _motorVehicleRepository = motorVehicleRepository;
            _chassisNumber = chassisNumber;
        }

        public async Task<IBusinessRuleResponse> Validate(CancellationToken cancellationToken = default)
        {
            var motorVehicle = await _motorVehicleRepository.FindByChassisNumberAsync(_chassisNumber, cancellationToken);

            if (motorVehicle is not null)
                return new BusinessRuleResponse { Messages = { $"The motor vehicle with given chassis number: {_chassisNumber}, already exists." } };

            return BusinessRuleResponse.Success;
        }
    }
}
