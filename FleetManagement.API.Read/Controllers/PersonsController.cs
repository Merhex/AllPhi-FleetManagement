using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FleetManagement.API.Read.Controllers
{
    [Route("api/read/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PersonsController(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}
