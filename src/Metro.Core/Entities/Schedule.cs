using Metro.Core.Entities.Base;

namespace Metro.Core.Entities
{
    public class Schedule : BaseEntity<Guid>
    {
        public Guid StationFromId { get; set; } //which is station ID
        public Guid StationToId { get; set; } //which is station ID
        public DateTime DepartureTime { get; set; }
        public int TotalSeat { get; set; }
        public int SeatBooked { get; set; }
        public int Price { get; set; }

        public virtual Station StationFrom { get; set; }
        public virtual Station StationTo { get; set; }
    }
}
