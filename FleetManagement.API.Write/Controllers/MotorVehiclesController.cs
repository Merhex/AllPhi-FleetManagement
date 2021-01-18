using FleetManagement.BLL.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FleetManagement.API.Write.Controllers
{
    [Route("api/write/[controller]")]
    [ApiController]
    public class MotorVehiclesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MotorVehiclesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateDriver(CreateMotorVehicleCommand command)
        {
            await _mediator.Send(command);

            return Ok();
        }
    }
}
