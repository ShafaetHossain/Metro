using MediatR;
using Shared.DTOs;

namespace Shared.Commands.Schedules
{
    public class CreateScheduleCommand : IRequest<ScheduleResponseDTO>
    {
        public Guid StationFromId { get; set; }
        public Guid StationToId { get; set; }
        public DateTime DepartureTime { get; set; }
        public int TotalSeat { get; set; }
        public int SeatBooked { get; set; }
        public int Price { get; set; }
    }
}
