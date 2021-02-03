using FleetManagement.DAL.Repositories.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.BLL
{
    public class MotorVehicleMileageDataValidation : IBusinessRule
    {
        private readonly IMotorVehicleMileageSnapshotRepository _repository;
        private readonly int _mileage;
        private readonly string _chassisNumber;

        public MotorVehicleMileageDataValidation(IMotorVehicleMileageSnapshotRepository repository, int mileage, string chassisNumber)
        {
            _repository = repository;
            _mileage = mileage;
            _chassisNumber = chassisNumber;
        }

        public async Task<IBusinessRuleResponse> Validate(CancellationToken cancellationToken = default)
        {
            var snapshot = await _repository.GetMileageForMotorVehicle(_chassisNumber, cancellationToken);

            var businessRuleResponse = BusinessRuleResponse.Success;

            if (snapshot is not null)
                if (_mileage >= snapshot.Mileage)
                    businessRuleResponse
                        .Failure(this, $"The given mileage, {_mileage}. Should be higher than latest recorded mileage: {snapshot.Mileage}");

            if (_mileage < 0)
                businessRuleResponse
                    .Failure(this, $"The given mileage, {_mileage}. Is not a valid value. Please enter a number higher than 0.");

            return businessRuleResponse;
        }
    }
}
