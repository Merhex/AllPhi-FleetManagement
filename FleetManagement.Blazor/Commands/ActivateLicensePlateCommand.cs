using System.Net.Http;

namespace FleetManagement.Blazor.Commands
{
    public record ActivateLicensePlateCommand : IApiCommand
    {
        public string Identifier { get; init; }

        public string Endpoint => "MotorVehicles/licensePlates/activate";
        public HttpMethod HttpMethod => HttpMethod.Patch;
    }
}
