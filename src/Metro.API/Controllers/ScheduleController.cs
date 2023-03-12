using MediatR;
using Metro.Application.Queries.Schedules;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Shared.Commands.Schedules;

namespace Metro.API.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ScheduleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSchedule([FromQuery] GetAllScheduleQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        [HttpPost]
        public async Task<IActionResult> CreateSchedule([FromBody] CreateScheduleCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
