namespace Shared.DTOs
{
    public class TicketInfoResponseDTO
    {
        public Guid Id { get; set; }
        public Guid ScheduleId { get; set; }
        public Guid UserId { get; set; }
        public int BuySeat { get; set; }
    }
}
