using FleetManagement.Blazor.Commands;
using FleetManagement.Blazor.Queries;
using FleetManagement.Blazor.Responses;
using System.Threading.Tasks;

namespace FleetManagement.Blazor.Services
{
    public interface IApiRequestService
    {
        Task<TReturn> SendGetRequest<TReturn>(IQuery query);
        Task<IApiCommandResponse> SendPostRequest<T>(string endpoint, T data);
        Task<IApiCommandResponse> SendPutRequest<T>(string endpoint, T data);
        Task<IApiCommandResponse> SendPatchRequest<T>(string endpoint, T data);
        Task<IApiCommandResponse> SendDeleteRequest<T>(string endpoint, T data);
    }
}
