using AutoMapper;
using MediatR;
using Metro.Application.Contracts.Repositories;
using Metro.Application.Contracts.Repositories.Command;
using Metro.Core.Entities;
using Shared.Commands.Users;
using Shared.DTOs;

namespace Metro.Application.CommandHandlers.Users
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserResponseDTO>
    {
        private readonly IUserCommandRepository _userCommandRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(IUserCommandRepository userCommandRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _userCommandRepository = userCommandRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UserResponseDTO> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            //map request type to entity(table) type so that we can insert
            var entity = _mapper.Map<User>(request);
            entity.Role = "User";

            //insert the entity and collect the response
            var newEntity = await _userCommandRepository.InsertAsync(entity);

            //commit changes
            await _unitOfWork.CommitAsync(cancellationToken);

            return _mapper.Map<UserResponseDTO>(newEntity);
        }
    }
}
