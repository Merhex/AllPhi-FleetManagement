using System.Net.Http;

namespace FleetManagement.WinForms.Commands
{
    public interface IApiCommand
    {
        public string Endpoint { get; }
        public HttpMethod HttpMethod { get; }
    }
}
