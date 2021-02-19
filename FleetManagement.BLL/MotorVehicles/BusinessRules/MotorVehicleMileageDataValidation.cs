using FleetManagement.BLL.MotorVehicles.Contracts;
using FleetManagement.DAL.Repositories.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.BLL
{
    public class MotorVehicleMileageDataValidation : IBusinessRule
    {
        private readonly IMotorVehicleMileageSnapshotRepository _repository;
        private readonly int _mileage;
        private readonly DateTime _dateTime;
        private readonly string _chassisNumber;

        public MotorVehicleMileageDataValidation(IMotorVehicleMileageSnapshotRepository repository, string chassisNumber, int mileage, DateTime dateTime)
        {
            _repository = repository;
            _mileage = mileage;
            _dateTime = dateTime;
            _chassisNumber = chassisNumber;
        }

        public async Task<IBusinessRuleResponse> Validate(CancellationToken cancellationToken = default)
        {
            var snapshot = await _repository.GetLastMileageOfMotorVehicle(_chassisNumber, cancellationToken);

            var businessRuleResponse = BusinessRuleResponse.Success;

            if (snapshot is not null)
            {
                if (IsEarlier(_dateTime, snapshot.SnapshotDate))
                    businessRuleResponse
                        .Failure(this, $"The given date: {_dateTime.ToShortDateString()}, is earlier than last given snapshot date: {snapshot.SnapshotDate.ToShortDateString()}.");

                if (_mileage <= snapshot.Mileage)
                    businessRuleResponse
                        .Failure(this, $"The given mileage, {_mileage}. Should be higher than latest recorded mileage: {snapshot.Mileage}");
            }

            if (_mileage < 0)
                businessRuleResponse
                    .Failure(this, $"The given mileage, {_mileage}. Is not a valid value. Please enter a number higher than 0.");

            return businessRuleResponse;
        }

        #region PRIVATE
        private static bool IsEarlier(DateTime dateTime1, DateTime dateTime2) => DateTime.Compare(dateTime1, dateTime2) <= 0;
        #endregion
    }
}
