using FleetManagement.DAL.Repositories.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.BLL
{
    public class LicensePlateNotInUse : IBusinessRule
    {
        private readonly ILicensePlateRepository _repository;
        private readonly string _identifier;

        public LicensePlateNotInUse(ILicensePlateRepository licensePlateRepository, string identifier)
        {
            _repository = licensePlateRepository;
            _identifier = identifier;
        }

        public async Task<IBusinessRuleResponse> Validate(CancellationToken cancellationToken = default)
        {
            var licensePlate = await _repository.FindByIdentifierAsync(_identifier, cancellationToken);

            if (licensePlate.InUse)
                return new BusinessRuleResponse 
                { 
                    Messages = { $"The license plate with identifier {_identifier} is in use. Please deattach the plate from the vehicle first." } 
                };

            return BusinessRuleResponse.Success;
        }
    }
}
