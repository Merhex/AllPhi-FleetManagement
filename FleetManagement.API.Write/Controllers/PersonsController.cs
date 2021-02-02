using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using FleetManagement.API.Write.Commands;

namespace FleetManagement.API.Write.Controllers
{
    [Route("api/write/[controller]")]
    [ApiController]
    public class PersonsController : WriteControllerBase
    {
        private readonly IMediator _mediator;

        public PersonsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdatePersonInformation(UpdatePersonInformationCommand command)
        {
            var response = await _mediator.Send(command);

            return WriteApiResponse(response);
        }
    }
}
