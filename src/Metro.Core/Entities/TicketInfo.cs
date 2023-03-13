using Metro.Core.Entities.Base;

namespace Metro.Core.Entities
{
    public class TicketInfo : BaseEntity<Guid>
    {
        public Guid ScheduleId { get; set; }
        public virtual Schedule Schedule { get; set; }
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public int BuySeat { get; set; }
    }
}
