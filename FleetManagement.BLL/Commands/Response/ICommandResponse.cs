using System.Collections.Generic;

namespace FleetManagement.BLL.Commands.Response
{
    public interface ICommandResponse
    {
        public string Type { get; init; }
        public int Status { get; init; }
        public string Title { get; init; }
        public IEnumerable<object> Errors { get; init; }
    }
}
