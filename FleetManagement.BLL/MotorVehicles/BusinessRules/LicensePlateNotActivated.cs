using FleetManagement.DAL.Repositories.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.BLL
{
    public class LicensePlateNotActivated : IBusinessRule
    {
        private readonly ILicensePlateRepository _repository;
        private readonly string _identifier;

        public LicensePlateNotActivated(ILicensePlateRepository licensePlateRepository, string identifier)
        {
            _repository = licensePlateRepository;
            _identifier = identifier;
        }

        public async Task<IBusinessRuleResponse> Validate(CancellationToken cancellationToken = default)
        {
            var licensePlate = await _repository.FindByIdentifierAsync(_identifier, cancellationToken);

            if (licensePlate is not null)
                if (licensePlate.InUse)
                    return new BusinessRuleResponse()
                        .Failure(this, $"The license plate with identifier {_identifier} is active. Please deattach the plate from the vehicle first.");

            return BusinessRuleResponse.Success;
        }
    }
}
