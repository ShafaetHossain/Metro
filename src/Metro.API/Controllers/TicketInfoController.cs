using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Shared.Commands.TicketInfoes;

namespace Metro.API.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]
    public class TicketInfoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TicketInfoController(IMediator mediator) 
        {
            _mediator = mediator;
        }

        [HttpPost("buyTicket")]
        public async Task<IActionResult> BuyTicket([FromBody] CreateTicketInfoCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
