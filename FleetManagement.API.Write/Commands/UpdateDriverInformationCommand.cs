using FleetManagement.BLL;
using FleetManagement.BLL.Drivers.Contracts;
using MediatR;
using System;

namespace FleetManagement.API.Write.Commands
{
    public record UpdateDriverInformationCommand : IRequest<IComponentResponse>, IUpdateDriverInformationContract
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
