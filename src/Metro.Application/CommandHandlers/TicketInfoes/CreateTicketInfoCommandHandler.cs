using AutoMapper;
using MediatR;
using Metro.Application.Contracts.Repositories;
using Metro.Application.Contracts.Repositories.Command;
using Metro.Application.Contracts.Repositories.Query;
using Metro.Core.Entities;
using Shared.Commands.TicketInfoes;
using Shared.DTOs;

namespace Metro.Application.CommandHandlers.TicketInfoes
{
    public class CreateTicketInfoCommandHandler : IRequestHandler<CreateTicketInfoCommand, TicketInfoResponseDTO>
    {
        private readonly ITicketInfoCommandRepository _ticketInfoCommandRepository;
        private readonly IScheduleQueryRepository _scheduleQueryRepository;
        private readonly IScheduleCommandRepository _scheduleCommandRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateTicketInfoCommandHandler(ITicketInfoCommandRepository ticketInfoCommandRepository, IScheduleQueryRepository scheduleQueryRepository, IScheduleCommandRepository scheduleCommandRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _ticketInfoCommandRepository = ticketInfoCommandRepository;
            _scheduleQueryRepository = scheduleQueryRepository;
            _scheduleCommandRepository = scheduleCommandRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<TicketInfoResponseDTO> Handle(CreateTicketInfoCommand request, CancellationToken cancellationToken)
        {
            var schedule = _scheduleQueryRepository.GetByIdAsync(request.ScheduleId);
            var scheduleEntity = _mapper.Map<Schedule>(schedule);

            if(scheduleEntity.TotalSeat - scheduleEntity.SeatBooked < request.BuySeat)
            {
                throw new Exception();
            }

            //map request type to entity(table) type so that we can insert
            var entity = _mapper.Map<TicketInfo>(request);
            //insert the entity and collect the response
            var newEntity = await _ticketInfoCommandRepository.InsertAsync(entity);
            //commit changes
            await _unitOfWork.CommitAsync(cancellationToken);

            //update the entity and collect the response
            scheduleEntity.SeatBooked += request.BuySeat;
            var updateScheduleEntity = _scheduleCommandRepository.UpdateAsync(scheduleEntity);
            //commit changes
            await _unitOfWork.CommitAsync(cancellationToken);

            return _mapper.Map<TicketInfoResponseDTO>(newEntity);
        }
    }
}
