using FleetManagement.Blazor.Queries;
using System.Threading.Tasks;

namespace FleetManagement.Blazor.Services
{
    public interface IApiRequestService
    {
        Task<TReturn> SendGetRequest<TReturn>(IQuery query);
    }
}
