using FleetManagement.BLL.MotorVehicles.Commands;
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
        public async Task<IActionResult> CreateMotorVehicle(CreateMotorVehicleCommand command)
        {
            var response = await _mediator.Send(command);

            return StatusCode(response.Status, response);
        }

        [HttpPost("licensePlates/create")]
        public async Task<IActionResult> CreateLicensePlate(CreateLicensePlateCommand command)
        {
            var response = await _mediator.Send(command);

            return StatusCode(response.Status, response);
        }

        [HttpPost("licensePlates/attach")]
        public async Task<IActionResult> AttachLicensePlate(AttachLicensePlateCommand command)
        {
            var response = await _mediator.Send(command);

            return StatusCode(response.Status, response);
        }

        [HttpPost("licensePlates/detach")]
        public async Task<IActionResult> DetachLicensePlate(DetachLicensePlaceCommand command)
        {
            var response = await _mediator.Send(command);

            return StatusCode(response.Status, response);
        }

        [HttpPatch("licensePlates/status")]
        public async Task<IActionResult> ChangeLicensePlateInUse(ChangeLicensePlateInUseStatusCommand command)
        {
            var response = await _mediator.Send(command);

            return StatusCode(response.Status, response);
        }

        [HttpPatch("operational")]
        public async Task<IActionResult> ChangeOperationalStatusMotorVehicle(ChangeMotorVehicleOperationalStatusCommand command)
        {
            var response = await _mediator.Send(command);

            return StatusCode(response.Status, response);
        }
    }
}
