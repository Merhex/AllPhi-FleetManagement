﻿using FleetManagement.DAL.Repositories.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.BLL
{
    public class LicensePlateNotAssigned : IBusinessRule
    {
        private readonly IMotorVehicleRepository _repository;
        private readonly string _licensePlateIdentifier;

        public LicensePlateNotAssigned(IMotorVehicleRepository repository, string licensePlateIdentifier)
        {
            _repository = repository;
            _licensePlateIdentifier = licensePlateIdentifier;
        }

        public async Task<IBusinessRuleResponse> Validate(CancellationToken cancellationToken = default)
        {
            var motorVehicle = await _repository.FindByLicensePlateIdentifierAsync(_licensePlateIdentifier, cancellationToken);

            if (motorVehicle is not null)
                return new BusinessRuleResponse()
                    .Failure(this, $"The license plate with identifier: {_licensePlateIdentifier}, is assigned. Please withdraw the plate first.");

            return BusinessRuleResponse.Success;
        }
    }
}
