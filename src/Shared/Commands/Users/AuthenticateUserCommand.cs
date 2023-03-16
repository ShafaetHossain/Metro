using MediatR;
using Shared.DTOs;

namespace Shared.Commands.Users
{
    public class AuthenticateUserCommand : IRequest<UserResponseDTO>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
