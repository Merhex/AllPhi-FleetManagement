using System;
using FleetManagement.BLL;
using FleetManagement.BLL.Persons.Contracts;
using MediatR;

namespace FleetManagement.API.Write.Commands
{
    public record UpdatePersonInformationCommand : IRequest<IComponentResponse>, IUpdatePersonInformationContract
    {
        public int DriverId { get; set; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public DateTime DateOfBirth { get; init; }
        public string NationalNumber { get; init; }
        public string AddressLine { get; init; }
        public string City { get; init; }
        public int ZipCode { get; init; }
    }
}
