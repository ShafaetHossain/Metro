using Metro.Application.Contracts;
using System.Security.Claims;

namespace Metro.API.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid? UserId => Guid.NewGuid(); /*new Guid(_httpContextAccessor.HttpContext?.User.FindFirstValue("sub"));*/
    }
}
