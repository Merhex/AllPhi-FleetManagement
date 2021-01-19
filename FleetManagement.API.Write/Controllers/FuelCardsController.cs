using FleetManagement.BLL.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FleetManagement.API.Write.Controllers
{
    [Route("api/write/[controller]")]
    [ApiController]
    public class FuelCardsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FuelCardsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateFuelCard(CreateFuelCardCommand command)
        {
            var response = await _mediator.Send(command);

            return StatusCode(response.Status, response);
        }
    }
}
