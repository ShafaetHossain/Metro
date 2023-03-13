using MediatR;
using Shared.DTOs;

namespace Shared.Commands.TicketInfoes
{
    public class CreateTicketInfoCommand : IRequest<TicketInfoResponseDTO>
    {
        public Guid ScheduleId { get; set; }
        public Guid UserId { get; set; }
        public int BuySeat { get; set; }
    }
}
