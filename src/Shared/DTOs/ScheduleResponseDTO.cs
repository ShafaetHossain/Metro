namespace Shared.DTOs
{
    public class ScheduleResponseDTO
    {
        public Guid Id { get; set; }
        public Guid StationFromId { get; set; }
        public Guid StationToId { get; set; }
        public DateTime DepartureTime { get; set; }
        public int TotalSeat { get; set; }
        public int SeatBooked { get; set; }
        public int Price { get; set; }
    }
}
