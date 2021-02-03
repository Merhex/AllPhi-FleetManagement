using FleetManagement.DAL.Repositories.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.BLL
{
    public class LicensePlateCannotExist : IBusinessRule
    {
        private readonly ILicensePlateRepository _repository;
        private readonly string _identifier;

        public LicensePlateCannotExist(ILicensePlateRepository repository, string identifier)
        {
            _repository = repository;
            _identifier = identifier;
        }

        public async Task<IBusinessRuleResponse> Validate(CancellationToken cancellationToken = default)
        {
            var licensePlate = await _repository.FindByIdentifierAsync(_identifier, cancellationToken);

            if (licensePlate is not null)
                return new BusinessRuleResponse()
                    .Failure(this, $"The license plate with given identifier: {_identifier}, already exists.");

            return BusinessRuleResponse.Success;
        }
    }
}
