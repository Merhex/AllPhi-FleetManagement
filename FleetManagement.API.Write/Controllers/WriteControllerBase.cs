using FleetManagement.BLL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FleetManagement.API.Write.Controllers
{
    [Authorize]
    public class WriteControllerBase : ControllerBase
    {
        protected IActionResult WriteApiResponse(IComponentResponse response)
        {
            if (response.Messages.Any()) 
                return BadRequest(new
                {
                    Errors = response.Messages
                });

            return Ok();
        }
    }
}
