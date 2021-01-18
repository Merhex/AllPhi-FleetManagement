using System.Collections;
using System.Collections.Generic;

namespace FleetManagement.Mappings
{
    public class ErrorResponse
    {
        public ICollection<ErrorModel> Errors { get; set; } = new List<ErrorModel>();
    }
}
