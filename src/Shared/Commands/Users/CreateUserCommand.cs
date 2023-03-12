using MediatR;
using Shared.DTOs;

namespace Shared.Commands.Users
{
    public class CreateUserCommand : IRequest<UserResponseDTO>
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
