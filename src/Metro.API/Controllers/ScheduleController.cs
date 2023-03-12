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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetScheduleById(Guid id)
        {
            return Ok(await _mediator.Send(new GetScheduleByIdQuery(id)));
        }

        [HttpPost]
        public async Task<IActionResult> CreateSchedule([FromBody] CreateScheduleCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSchedule(Guid id, [FromBody] UpdateScheduleCommand command)
        {
            if(command.Id == id)
            {
                return Ok(await _mediator.Send(command));
            }
            return BadRequest();
        }
    }
}
