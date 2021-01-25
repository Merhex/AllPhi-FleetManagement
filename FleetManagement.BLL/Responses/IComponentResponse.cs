using System.Collections.Generic;

namespace FleetManagement.BLL
{
    public interface IComponentResponse
    {
        public string Reference { get; }
        public int Status { get; }
        public string Title { get; }
        public IList<IErrorResponseModel> Errors { get; }
    }
}