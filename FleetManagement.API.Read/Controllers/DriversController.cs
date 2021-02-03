using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FleetManagement.API.Read.Controllers
{
    [Route("api/read/[controller]")]
    [ApiController]
    public class DriversController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DriversController(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}
