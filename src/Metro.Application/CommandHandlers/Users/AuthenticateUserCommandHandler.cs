using AutoMapper;
using MediatR;
using Metro.Application.Common.Constants;
using Metro.Application.Common.Exceptions;
using Metro.Application.Contracts.Repositories.Query.Base;
using Shared.Commands.Users;
using Shared.DTOs;

namespace Metro.Application.CommandHandlers.Users
{
    public class AuthenticateUserCommandHandler : IRequestHandler<AuthenticateUserCommand, UserResponseDTO>
    {
        private readonly IUserQueryRepository _userQueryRepository;
        private readonly IMapper _mapper;

        public AuthenticateUserCommandHandler(IUserQueryRepository userQueryRepository, IMapper mapper)
        {
            _userQueryRepository = userQueryRepository;
            _mapper = mapper;
        }

        public async Task<UserResponseDTO> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userQueryRepository.GetByEmailAsync(request);
            if(user == null)
            {
                throw new NotFoundException(ValidationError.UserNotFound);
            }
            return _mapper.Map<UserResponseDTO>(user);
        }
    }
}
