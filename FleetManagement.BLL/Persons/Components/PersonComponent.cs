using FleetManagement.BLL.Persons.Components.Interfaces;
using FleetManagement.BLL.Persons.Contracts;
using FleetManagement.BLL.Persons.Validators;
using FleetManagement.DAL.NHibernate;
using FleetManagement.DAL.Repositories.Interfaces;
using FleetManagement.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.BLL.Persons.Components
{
    public class PersonComponent : IPersonComponent
    {
        private readonly IPersonSession _personSession;
        private readonly IBusinessHandler _businessHandler;

        public PersonComponent(IPersonSession personSession,
            IBusinessHandler businessHandler)
        {
            _personSession = personSession;
            _businessHandler = businessHandler;
        }

        public async Task<IComponentResponse> UpdatePersonInformationAsync(IUpdatePersonInformationContract contract, CancellationToken cancellationToken)
        {
            var handlerResponse = await _businessHandler.Validate(contract, cancellationToken);

            if (handlerResponse.Success)
            {
                await UpdatePersonInformation(contract, cancellationToken);

                return ComponentResponse.Success;
            }
            else
            {
                return new ComponentResponse()
                    .WithResponse(handlerResponse);
            }
        }

        #region PRIVATE
        private async Task UpdatePersonInformation(IUpdatePersonInformationContract contract, CancellationToken cancellationToken)
        {
            try
            {
                _personSession.BeginTransaction();

                var person = await _personSession.GetPersonByNationalNumber(contract.NationalNumber, cancellationToken);

                person.FirstName = contract.FirstName;
                person.LastName = contract.LastName;
                person.DateOfBirth = contract.DateOfBirth;
                person.AddressLine = contract.AddressLine;
                person.City = contract.City;
                person.ZipCode = contract.ZipCode;

                await _personSession.Save(person, cancellationToken);
                await _personSession.Commit(cancellationToken);
            }
            catch (Exception)
            {
                await _personSession.Rollback(cancellationToken);
            }
            finally
            {
                _personSession.CloseTransaction();
            }

        }
        #endregion
    }
}
