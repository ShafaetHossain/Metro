using Metro.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metro.Core.Entities
{
    public class TicketInfo : BaseEntity<Guid>
    {
        public Guid ScheduleId { get; set; }
        public virtual Schedule Schedule { get; set; }
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
    }
}
