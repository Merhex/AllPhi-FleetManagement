using FleetManagement.BLL.Commands.Response;
using FleetManagement.Mappings;
using FleetManagement.Models;
using MediatR;
using System;

namespace FleetManagement.BLL.Commands
{
    public record CreateDriverCommand : IRequest<ICommandResponse>
    {
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
