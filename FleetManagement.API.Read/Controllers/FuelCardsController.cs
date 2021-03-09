using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FleetManagement.API.Read.Controllers
{
    [Route("api/read/[controller]")]
    [ApiController]
    public class FuelCardsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FuelCardsController(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}
