using FleetManagement.BLL.Drivers.Components.Interfaces;
using FleetManagement.BLL.Drivers.Contracts;
using FleetManagement.DAL.NHibernate;
using FleetManagement.Models;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.BLL.Drivers.Components
{
    public class DriverComponent : IDriverComponent
    {
        private readonly IDriverSession _driverSession;
        private readonly IBusinessHandler _businessHandler;

        public DriverComponent(IDriverSession driverSession,
            IBusinessHandler businessHandler)
        {
            _driverSession = driverSession;
            _businessHandler = businessHandler;
        }

        public Task<IComponentResponse> ChangeDriverActivityAsync(IChangeDriverActivityStatusContract contract, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IComponentResponse> CreateDriverAsync(ICreateDriverContract contract, CancellationToken cancellationToken)
        {
            var handlerResponse = await _businessHandler.Validate(contract, cancellationToken);

            if (handlerResponse.Success)
            {
                await CreateDriver(contract, cancellationToken);

                return ComponentResponse.Success;
            }
            else
            {
                return new ComponentResponse()
                    .WithResponse(handlerResponse);
            }
        }

        private async Task CreateDriver(ICreateDriverContract contract, CancellationToken cancellationToken)
        {
            var driver = CreateDriver(contract);

            await _driverSession.Save(driver, cancellationToken);
        }

        private static Driver CreateDriver(ICreateDriverContract contract)
        {
            return new Driver
            {
                Active = true,
                AddressLine = contract.AddressLine,
                City = contract.City,
                DateOfBirth = contract.DateOfBirth,
                FirstName = contract.FirstName,
                LastName = contract.LastName,
                NationalNumber = contract.NationalNumber
            };
        }
    }
}
