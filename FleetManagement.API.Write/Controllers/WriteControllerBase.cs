using FleetManagement.BLL;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FleetManagement.API.Write.Controllers
{
    public class WriteControllerBase : ControllerBase
    {
        protected IActionResult WriteApiResponse(IComponentResponse response)
        {
            if (response.Failures.Any()) 
                return BadRequest(new
                {
                    Errors = response.Failures
                });

            return Ok();
        }
    }
}
