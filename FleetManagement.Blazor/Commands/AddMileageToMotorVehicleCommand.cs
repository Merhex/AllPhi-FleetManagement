using System;
using System.Net.Http;

namespace FleetManagement.Blazor.Commands
{
    public record AddMileageToMotorVehicleCommand : IApiCommand
    {
        public int Mileage { get; set; }
        public DateTime Date { get; set; }
        public string ChassisNumber { get; set; }

        public string Endpoint => "MotorVehicles/mileage/add";
        public HttpMethod HttpMethod { get; set; } = HttpMethod.Post;
    }
}
