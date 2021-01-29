using FleetManagement.BLL.MotorVehicles.Contracts;
using FleetManagement.DAL.Repositories.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.BLL
{
    public class LicensePlateExists : IBusinessRule<ILicensePlateContract>
    {
        public event BusinessRuleFailureEventHandler<ILicensePlateContract> Failure;

        private readonly ILicensePlateRepository _licensePlateRepository;

        public LicensePlateExists(ILicensePlateRepository licensePlateRepository)
        {
            _licensePlateRepository = licensePlateRepository;
        }

        public async Task Handle(ILicensePlateContract contract, CancellationToken token = default)
        {
            var licensePlate = await _licensePlateRepository.FindByIdentifierAsync(contract.Identifier, token);

            if (licensePlate is not null)
            {
                OnFailure(new BusinessRuleFailureEventArgs
                {
                    Messages = { $"The license plate with given identifier {contract.Identifier} already exists." }
                });
            }
        }

        private void OnFailure(BusinessRuleFailureEventArgs args)
        {
            Failure?.Invoke(this, args);
        }
    }
}
