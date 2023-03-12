using AutoMapper;
using MediatR;
using Metro.Application.Common.Exceptions;
using Metro.Application.Contracts.Repositories.Query;
using Metro.Core.Entities;
using Shared.DTOs;

namespace Metro.Application.Queries.Schedules
{
    public class GetScheduleByIdQuery : IRequest<ScheduleResponseDTO>
    {
        public GetScheduleByIdQuery(Guid id) 
        {
            Id = id;
        }
        public Guid Id { get; set; }
    }

    public class GetScheduleByIdQueryHandler : IRequestHandler<GetScheduleByIdQuery, ScheduleResponseDTO>
    {
        private readonly IScheduleQueryRepository _scheduleQueryRepository;
        private readonly IMapper _mapper;

        public GetScheduleByIdQueryHandler(IScheduleQueryRepository scheduleQueryRepository, IMapper mapper)
        {
            _scheduleQueryRepository = scheduleQueryRepository;
            _mapper = mapper;
        }

        public async Task<ScheduleResponseDTO> Handle(GetScheduleByIdQuery request, CancellationToken cancellationToken)
        {
            var schedule = await _scheduleQueryRepository.GetByIdAsync(request.Id);
            if(schedule == null)
            {
                throw new NotFoundException("Schedule not found");
            }
            return _mapper.Map<Schedule, ScheduleResponseDTO>(schedule);
        }
    }
}
