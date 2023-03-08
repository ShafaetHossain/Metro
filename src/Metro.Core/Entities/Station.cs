using Metro.Core.Entities.Base;

namespace Metro.Core.Entities
{
    public class Station : BaseEntity<Guid>
    {
        public string StationName { get; set; }
    }
}
