using System.Net.Http;

namespace FleetManagement.Blazor.Commands
{
    public record AssignLicensePlateToMotorVehicleCommand : IApiCommand
    {
        public string ChassisNumber { get; set; }
        public string Identifier { get; set; }

        public string Endpoint => "MotorVehicles/licensePlates/assign";
        public HttpMethod HttpMethod => HttpMethod.Post;
    }
}
