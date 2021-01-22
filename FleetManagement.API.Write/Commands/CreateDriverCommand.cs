using System;
using FleetManagement.BLL;
using FleetManagement.BLL.Drivers.Contracts;
using MediatR;

namespace FleetManagement.API.Write.Commands
{
    public record CreateDriverCommand : ICreateDriverContract, IRequest<IComponentResponse>
    {
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public DateTime DateOfBirth { get; init; }
        public string NationalNumber { get; init; }
        public string AddressLine { get; init; }
        public string City { get; init; }
        public int ZipCode { get; init; }
    }
}
