using FleetManagement.DAL.Repositories.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.BLL
{
    public class MotorVehicleNotActiveLicensePlate : IBusinessRule
    {
        private readonly IMotorVehicleRepository _repository;
        private readonly string _identifier;

        public MotorVehicleNotActiveLicensePlate(IMotorVehicleRepository repository, string identifier)
        {
            _repository = repository;
            _identifier = identifier;
        }

        public async Task<IBusinessRuleResponse> Validate(CancellationToken cancellationToken = default)
        {
            var motorVehicle = await _repository.FindByLicensePlateIdentifierIncludeLicensePlatesAsync(_identifier, cancellationToken);

            if (motorVehicle is not null)
                if (motorVehicle.LicensePlates.Any(licensePlate => licensePlate.InUse))
                {
                    var licensePlateInUse = motorVehicle.LicensePlates.First(licensePlate => licensePlate.InUse);

                    return new BusinessRuleResponse()
                        .Failure(this, $"The motor vehicle with chassis number: {motorVehicle.ChassisNumber}, already has license plate with identifier: {licensePlateInUse.Identifier} attached.");
                }

            return BusinessRuleResponse.Success;
        }
    }
}
