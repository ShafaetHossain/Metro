using MediatR;
using Metro.Application.Common.Constants;
using Metro.Application.Common.Exceptions;
using Metro.Application.Contracts.Repositories;
using Metro.Application.Contracts.Repositories.Command;
using Metro.Application.Contracts.Repositories.Query;
using Shared.Commands.Schedules;

namespace Metro.Application.CommandHandlers.Schedules
{
    public class DeleteScheduleCommandHandler : IRequestHandler<DeleteScheduleCommand, string>
    {
        private readonly IScheduleCommandRepository _scheduleCommandRepository;
        private readonly IScheduleQueryRepository _scheduleQueryRepository;
        IUnitOfWork _unitOfWork;

        public DeleteScheduleCommandHandler(IScheduleCommandRepository scheduleCommandRepository, IScheduleQueryRepository scheduleQueryRepository, IUnitOfWork unitOfWork)
        {
            _scheduleCommandRepository = scheduleCommandRepository;
            _scheduleQueryRepository = scheduleQueryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<string> Handle(DeleteScheduleCommand request, CancellationToken cancellationToken)
        {
            var scheduleEntity = await _scheduleQueryRepository.GetByIdAsync(request.Id);
            if(scheduleEntity == null) 
            {
                throw new NotFoundException(ValidationError.ScheduleNotFound);
            }

            scheduleEntity.IsDeleted = true;

            await _scheduleCommandRepository.UpdateAsync(scheduleEntity);

            await _unitOfWork.CommitAsync(cancellationToken);

            return ValidationError.ScheduleDeleted;
        }
    }
}
