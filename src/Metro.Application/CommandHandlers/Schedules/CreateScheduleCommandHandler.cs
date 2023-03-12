using AutoMapper;
using MediatR;
using Metro.Application.Contracts.Repositories;
using Metro.Application.Contracts.Repositories.Command;
using Metro.Core.Entities;
using Shared.Commands.Schedules;
using Shared.DTOs;

namespace Metro.Application.CommandHandlers.Schedules
{
    public class CreateScheduleCommandHandler : IRequestHandler<CreateScheduleCommand, ScheduleResponseDTO>
    {
        private readonly IScheduleCommandRepository _scheduleCommandRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateScheduleCommandHandler(IScheduleCommandRepository scheduleCommandRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _scheduleCommandRepository = scheduleCommandRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ScheduleResponseDTO> Handle(CreateScheduleCommand request, CancellationToken cancellationToken)
        {
            //map request type to entity(table) type so that we can insert
            var entity = _mapper.Map<Schedule>(request);

            //insert the entity and collect the response
            var newEntity = await _scheduleCommandRepository.InsertAsync(entity);

            //commit changes
            await _unitOfWork.CommitAsync(cancellationToken);

            return _mapper.Map<ScheduleResponseDTO>(newEntity);
        }
    }
}
