using FleetManagement.BLL.Drivers.Components.Interfaces;
using FleetManagement.BLL.Drivers.Contracts;
using FleetManagement.DAL.NHibernate;
using FleetManagement.Models;
using NHibernate.Linq;
using System;
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

        public async Task<IComponentResponse> ActivateDriverAsync(IActivateDriverContract contract, CancellationToken cancellationToken)
        {
            var handlerResponse = await _businessHandler.Validate(contract, cancellationToken);

            if (handlerResponse.Success)
            {
                await ActivateDriver(contract, cancellationToken);

                return ComponentResponse.Success;
            }
            else
            {
                return new ComponentResponse()
                    .WithResponse(handlerResponse);
            }
        }

        public async Task<IComponentResponse> DeactivateDriverAsync(IDeactivateDriverContract contract, CancellationToken cancellationToken)
        {
            var handlerResponse = await _businessHandler.Validate(contract, cancellationToken);

            if (handlerResponse.Success)
            {
                await DeactivateDriver(contract, cancellationToken);

                return ComponentResponse.Success;
            }
            else
            {
                return new ComponentResponse()
                    .WithResponse(handlerResponse);
            }
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

        #region PRIVATE
        private async Task ActivateDriver(IActivateDriverContract contract, CancellationToken cancellationToken)
        {
            try
            {
                _driverSession.BeginTransaction();

                var driver = await _driverSession.GetDriverByNationalNumber(contract.NationalNumber, cancellationToken);
                driver.Active = true;

                await _driverSession.Save(driver, cancellationToken);
                await _driverSession.Commit(cancellationToken);
            }
            catch (Exception)
            {
                await _driverSession.Rollback(cancellationToken);
            }
            finally
            {
                _driverSession.CloseTransaction();
            }
        }

        private async Task DeactivateDriver(IDeactivateDriverContract contract, CancellationToken cancellationToken)
        {
            try
            {
                _driverSession.BeginTransaction();

                var driver = await _driverSession.GetDriverByNationalNumber(contract.NationalNumber, cancellationToken);
                driver.Active = false;

                await _driverSession.Save(driver, cancellationToken);
                await _driverSession.Commit(cancellationToken);

            }
            catch (Exception)
            {
                await _driverSession.Rollback(cancellationToken);
            }
            finally
            {
                _driverSession.CloseTransaction();

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
                NationalNumber = contract.NationalNumber,
                ZipCode = contract.ZipCode 
            };
        }
        #endregion
    }
}
