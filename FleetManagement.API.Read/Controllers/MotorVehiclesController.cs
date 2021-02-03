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
        public async Task<IActionResult> GetOperationalMotorVehicles([FromQuery] AllOperationalMotorVehiclesQuery query)
        {
            var result = await _mediator.Send(query);

            return Ok(result);
        }
    }
}
