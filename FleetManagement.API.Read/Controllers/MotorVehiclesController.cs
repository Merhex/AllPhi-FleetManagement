using FleetManagement.API.Read.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FleetManagement.API.Read.Controllers
{
    [Route("api/read/[controller]")]
    [ApiController]
    public class MotorVehiclesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MotorVehiclesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetMotorVehicles([FromQuery] MotorVehiclesQuery query)
        {
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpGet("detailed")]
        public async Task<IActionResult> GetDetailedMotorVehicle([FromQuery] MotorVehicleDetailedQuery query)
        {
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpGet("licensePlates")]
        public async Task<IActionResult> GetLicensePlates([FromQuery] LicensePlatesQuery query)
        {
            var result = await _mediator.Send(query);

            return Ok(result);
        }


        [HttpGet("licensePlate/detailed")]
        public async Task<IActionResult> GetLicensePlate([FromQuery] LicensePlateDetailedQuery query)
        {
            var result = await _mediator.Send(query);

            return Ok(result);
        }
    }
}
