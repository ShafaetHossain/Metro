using Metro.Core.Entities.Base;

namespace Metro.Core.Entities
{
    public class User : BaseEntity<Guid>
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
