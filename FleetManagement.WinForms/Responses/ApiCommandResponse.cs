using System.Collections.Generic;

namespace FleetManagement.WinForms.Responses
{
    public class ApiCommandResponse : IApiCommandResponse
    {
        public IDictionary<string, IEnumerable<string>> Errors { get; set; } = new Dictionary<string, IEnumerable<string>>();
    }
}
