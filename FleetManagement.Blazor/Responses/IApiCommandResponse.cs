using System.Collections.Generic;

namespace FleetManagement.Blazor.Responses
{
    public interface IApiCommandResponse
    {
        IDictionary<string, IEnumerable<string>> Errors { get; set; }
    }
}
