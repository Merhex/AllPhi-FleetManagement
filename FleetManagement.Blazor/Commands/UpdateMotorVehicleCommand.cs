using System.Net.Http;

namespace FleetManagement.Blazor.Commands
{
    public record UpdateMotorVehicleCommand : IApiCommand
    {
        public string Brand { get; init; }
        public string Model { get; init; }
        public bool Operational { get; init; }
        public int BodyType { get; init; }
        public int PropulsionType { get; init; }
        public string ChassisNumber { get; set; }

        public string Endpoint => "MotorVehicles/update";
        public HttpMethod HttpMethod => HttpMethod.Put;
    }
}
