using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using FleetManagement.API.Write.Commands;

namespace FleetManagement.API.Write.Controllers
{
    [Route("api/write/[controller]")]
    [ApiController]
    public class MotorVehiclesController : WriteControllerBase
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

            return WriteApiResponse(response);
        }

        [HttpPost("licensePlates/create")]
        public async Task<IActionResult> CreateLicensePlate(CreateLicensePlateCommand command)
        {
            var response = await _mediator.Send(command);

            return WriteApiResponse(response);
        }

        [HttpPost("licensePlates/assign")]
        public async Task<IActionResult> AssignLicensePlate(AssignLicensePlateCommand command)
        {
            var response = await _mediator.Send(command);

            return WriteApiResponse(response);
        }

        [HttpPost("licensePlates/withdraw")]
        public async Task<IActionResult> WithdrawLicensePlate(WithdrawLicensePlateCommand command)
        {
            var response = await _mediator.Send(command);

            return WriteApiResponse(response);
        }

        [HttpPatch("licensePlates/status")]
        public async Task<IActionResult> ChangeLicensePlateInUse(ChangeLicensePlateInUseStatusCommand command)
        {
            var response = await _mediator.Send(command);

            return WriteApiResponse(response);
        }

        [HttpDelete("licensePlates/delete")]
        public async Task<IActionResult> DeleteLicensePlate(DeleteLicensePlateCommand command)
        {
            var response = await _mediator.Send(command);

            return WriteApiResponse(response);
        }

        [HttpPatch("operational")]
        public async Task<IActionResult> ChangeOperationalStatusMotorVehicle(ChangeMotorVehicleOperationalStatusCommand command)
        {
            var response = await _mediator.Send(command);

            return WriteApiResponse(response);
        }
    }
}
