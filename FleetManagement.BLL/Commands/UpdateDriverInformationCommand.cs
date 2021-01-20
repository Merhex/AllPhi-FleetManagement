using FleetManagement.BLL.Commands.Response;
using FleetManagement.Models;
using MediatR;
using System;

namespace FleetManagement.BLL.Commands
{
    public record UpdateDriverInformationCommand : IRequest<ICommandResponse>
    {
        public int DriverId { get; set; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public DateTime DateOfBirth { get; init; }
        public string NationalNumber { get; init; }
        public string AddressLine { get; init; }
        public string City { get; init; }
        public int ZipCode { get; init; }

        public DriverLicense DriverLicense { get; init; }
    }
}
