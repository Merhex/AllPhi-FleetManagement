using FleetManagement.WinForms.Queries;
using FleetManagement.WinForms.Responses;
using System.Threading.Tasks;

namespace FleetManagement.WinForms.Services
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
