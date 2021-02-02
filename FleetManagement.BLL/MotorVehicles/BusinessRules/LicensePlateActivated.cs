using FleetManagement.DAL.Repositories.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.BLL
{
    public class LicensePlateActivated : IBusinessRule
    {
        private readonly ILicensePlateRepository _repository;
        private readonly string _identifier;

        public LicensePlateActivated(ILicensePlateRepository licensePlateRepository, string identifier)
        {
            _repository = licensePlateRepository;
            _identifier = identifier;
        }

        public async Task<IBusinessRuleResponse> Validate(CancellationToken cancellationToken = default)
        {
            var licensePlate = await _repository.FindByIdentifierAsync(_identifier, cancellationToken);

            if (licensePlate is not null)
                if (licensePlate.InUse is false)
                    return new BusinessRuleResponse()
                        .Failure(this, $"The license plate with identifier {_identifier} is not active. Please attach the plate on a vehicle first.");

            return BusinessRuleResponse.Success;
        }
    }
}
