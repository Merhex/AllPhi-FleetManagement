using FleetManagement.Blazor.Commands;
using FleetManagement.Blazor.Queries;
using FleetManagement.Blazor.Responses;
using System.Threading.Tasks;

namespace FleetManagement.Blazor.Services
{
    public interface IApiRequestService
    {
        Task<TReturn> SendQuery<TReturn>(IQuery query);
        Task<IApiCommandResponse> SendCommand(IApiCommand command);
    }
}
