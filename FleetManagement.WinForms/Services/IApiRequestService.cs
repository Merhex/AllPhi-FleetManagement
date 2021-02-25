using FleetManagement.WinForms.Commands;
using FleetManagement.WinForms.Queries;
using FleetManagement.WinForms.Responses;
using System.Threading.Tasks;

namespace FleetManagement.WinForms.Services
{
    public interface IApiRequestService
    {
        Task<TReturn> SendQuery<TReturn>(IQuery query);
        Task<IApiCommandResponse> SendCommand(IApiCommand command);
    }
}
