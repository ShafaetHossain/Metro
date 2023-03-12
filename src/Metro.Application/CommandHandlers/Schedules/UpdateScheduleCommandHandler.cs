using AutoMapper;
using MediatR;
using Metro.Application.Common.Exceptions;
using Metro.Application.Contracts.Repositories;
using Metro.Application.Contracts.Repositories.Command;
using Metro.Application.Contracts.Repositories.Query;
using Metro.Core.Entities;
using Shared.Commands.Schedules;
using Shared.DTOs;

namespace Metro.Application.CommandHandlers.Schedules
{
    public class UpdateScheduleCommandHandler : IRequestHandler<UpdateScheduleCommand, ScheduleResponseDTO>
    {
        private readonly IScheduleCommandRepository _scheduleCommandRepository;
        private readonly IScheduleQueryRepository _scheduleQueryRepository;

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateScheduleCommandHandler(IScheduleCommandRepository scheduleCommandRepository, IScheduleQueryRepository scheduleQueryRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _scheduleCommandRepository = scheduleCommandRepository;
            _scheduleQueryRepository = scheduleQueryRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ScheduleResponseDTO> Handle(UpdateScheduleCommand request, CancellationToken cancellationToken)
        {
            var currentSchedule = await _scheduleQueryRepository.GetByIdAsync(request.Id);
            if(currentSchedule == null)
            {
                throw new NotFoundException("Schedule not found");
            }
            //map request type to entity(table) type so that we can update
            var scheduleEntity = _mapper.Map<UpdateScheduleCommand, Schedule>(request, currentSchedule);

            //update the entity and collect the response
            var updateScheduleEntity = _scheduleCommandRepository.UpdateAsync(scheduleEntity);

            //commit changes
            await _unitOfWork.CommitAsync(cancellationToken);

            return _mapper.Map<ScheduleResponseDTO>(updateScheduleEntity.Result);
        }
    }
}
