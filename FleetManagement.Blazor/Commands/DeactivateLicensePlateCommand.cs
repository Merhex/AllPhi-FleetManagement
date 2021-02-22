using System.Net.Http;

namespace FleetManagement.Blazor.Commands
{
    public record DeactivateLicensePlateCommand : IApiCommand
    {
        public string Identifier { get; init; }

        public string Endpoint => "MotorVehicles/licensePlates/deactivate";
        public HttpMethod HttpMethod => HttpMethod.Patch;
    }
}
