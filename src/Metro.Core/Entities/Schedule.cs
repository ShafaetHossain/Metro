using Metro.Core.Entities.Base;

namespace Metro.Core.Entities
{
    public class Schedule : BaseEntity<Guid>
    {
        public Guid StationFrom { get; set; } //which is station ID
        public Guid StationTo { get; set; } //which is station ID
        public DateTime DepartureTime { get; set; }
        public int TotalSeat { get; set; }
        public int? SeatBooked { get; set; } = 0;
        public int Price { get; set; }

        public Station Station { get; set; } 
    }
}
