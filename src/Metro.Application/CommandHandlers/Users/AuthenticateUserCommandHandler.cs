using MediatR;
using Metro.Application.Common.Constants;
using Metro.Application.Common.Exceptions;
using Metro.Application.Contracts.Repositories.Query.Base;
using Shared.Commands.Users;

namespace Metro.Application.CommandHandlers.Users
{
    public class AuthenticateUserCommandHandler : IRequestHandler<AuthenticateUserCommand, string>
    {
        private readonly IUserQueryRepository _userQueryRepository;

        public AuthenticateUserCommandHandler(IUserQueryRepository userQueryRepository)
        {
            _userQueryRepository = userQueryRepository;
        }

        public async Task<string> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userQueryRepository.GetByEmailAsync(request);
            if(user == null)
            {
                throw new NotFoundException(ValidationError.UserNotFound);
            }
            return ValidationError.UserLoginSuccess;
        }
    }
}
