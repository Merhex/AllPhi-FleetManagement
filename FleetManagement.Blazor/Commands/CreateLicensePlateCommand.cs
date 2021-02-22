using System.Net.Http;

namespace FleetManagement.Blazor.Commands
{
    public record CreateLicensePlateCommand : IApiCommand
    {
        public string Identifier { get; init; }

        public string Endpoint => "MotorVehicles/licensePlates/create";
        public HttpMethod HttpMethod => HttpMethod.Post;
    }
}
