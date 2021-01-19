using FleetManagement.Mappings;
using FleetManagement.Models;
using MediatR;
using System;

namespace FleetManagement.BLL.Commands
{
    public record UpdateDriverInformationCommand : IRequest<CommandResponse>
    {
        public int DriverId { get; set; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public DateTime DateOfBirth { get; init; }
        public string NationalNumber { get; init; }
        public string Street { get; init; }
        public string City { get; init; }
        public int ZipCode { get; init; }

        public DriverLicense DriverLicense { get; init; }
    }
}
