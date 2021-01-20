using FleetManagement.BLL.Drivers.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FleetManagement.API.Write.Controllers
{
    [Route("api/write/[controller]")]
    [ApiController]
    public class DriversController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DriversController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateDriver(CreateDriverCommand command)
        {
            var response = await _mediator.Send(command);

            return StatusCode(response.Status, response);
        }

        [HttpPut("activity")]
        public async Task<IActionResult> ChangeActivityOfDriver(ChangeDriverActivityStatusCommand command)
        {
            var response = await _mediator.Send(command);

            return StatusCode(response.Status, response);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateDriverInformation(UpdateDriverInformationCommand command)
        {
            var response = await _mediator.Send(command);

            return StatusCode(response.Status, response);
        }
    }
}
