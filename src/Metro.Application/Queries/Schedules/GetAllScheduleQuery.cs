using AutoMapper;
using MediatR;
using Metro.Application.Contracts.Repositories.Query;
using Shared.DTOs;

namespace Metro.Application.Queries.Schedules
{
    public class GetAllScheduleQuery : IRequest<PaginatedListResponseDTO<ScheduleResponseDTO>>
    {
        //maximum allowed pageSize
        const int MAX_PAGE_SIZE = 50;
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 1;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > MAX_PAGE_SIZE) ? MAX_PAGE_SIZE : value;
            }
        }
    }

    public class GetAllScheduleQueryHandler : IRequestHandler<GetAllScheduleQuery, PaginatedListResponseDTO<ScheduleResponseDTO>>
    {
        private readonly IScheduleQueryRepository _scheduleQueryRepository;
        private readonly IMapper _mapper;

        public GetAllScheduleQueryHandler(IScheduleQueryRepository scheduleQueryRepository, IMapper mapper)
        {
            _scheduleQueryRepository = scheduleQueryRepository;
            _mapper = mapper;
        }

        public async Task<PaginatedListResponseDTO<ScheduleResponseDTO>> Handle(GetAllScheduleQuery request, CancellationToken cancellationToken)
        {
            var (count, list) = await _scheduleQueryRepository.GetAllAsync(request);
            var scheduleList = _mapper.Map<IEnumerable<ScheduleResponseDTO>>(list);
            return new PaginatedListResponseDTO<ScheduleResponseDTO>(scheduleList, count, request.PageNumber, request.PageSize);
        }
    }
}
