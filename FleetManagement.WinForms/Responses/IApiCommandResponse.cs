using System.Collections.Generic;

namespace FleetManagement.WinForms.Responses
{
    public interface IApiCommandResponse
    {
        IDictionary<string, IEnumerable<string>> Errors { get; set; }
    }
}
