using MediatR;
using Metro.Application.Queries.Stations;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Shared.Commands.Stations;

namespace Metro.API.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]
    public class StationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStation([FromQuery] GetAllStationQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStationById(Guid id)
        {
            return Ok(await _mediator.Send(new GetStationByIdQuery(id)));
        }

        [HttpPost]
        public async Task<IActionResult> CreateStation([FromBody] CreateStationCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStation(Guid id, [FromBody] UpdateStationCommand command)
        {
            if(command.Id == id)
            {
                return Ok(await _mediator.Send(command));
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStation(Guid id)
        {
            var response = await _mediator.Send(new DeleteStationCommand(id));
            return Ok(new { result = response });
        }
    }
}
