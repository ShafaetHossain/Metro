using MediatR;

namespace Shared.Commands.Users
{
    public class AuthenticateUserCommand : IRequest<string>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
