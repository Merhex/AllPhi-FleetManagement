using FleetManagement.DAL.Repositories.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.BLL
{
    public class MotorVehicleExists : IBusinessRule
    {
        private readonly IMotorVehicleRepository _motorVehicleRepository;
        private readonly string _chassisNumber;

        public MotorVehicleExists(IMotorVehicleRepository motorVehicleRepository, string chassisNumber)
        {
            _motorVehicleRepository = motorVehicleRepository;
            _chassisNumber = chassisNumber;
        }

        public async Task<IBusinessRuleResponse> Validate(CancellationToken cancellationToken = default)
        {
            var motorVehicle = await _motorVehicleRepository.FindByChassisNumberAsync(_chassisNumber, cancellationToken);

            if (motorVehicle is null)
                return new BusinessRuleResponse()
                    .Failure(this, $"The motor vehicle with given chassis number: {_chassisNumber}, does not exist.");

            return BusinessRuleResponse.Success;
        }
    }
}
