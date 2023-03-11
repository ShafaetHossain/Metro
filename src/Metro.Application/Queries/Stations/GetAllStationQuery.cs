using AutoMapper;
using MediatR;
using Metro.Application.Contracts.Repositories.Query;
using Shared.DTOs;

namespace Metro.Application.Queries.Stations
{
    public class GetAllStationQuery : IRequest<PaginatedListResponseDTO<StationResponseDTO>>
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
        public string? StationName { get; set; }
    }

    public class GetAllStationQueryHandler : IRequestHandler<GetAllStationQuery, PaginatedListResponseDTO<StationResponseDTO>>
    {
        private readonly IStationQueryRepository _stationQueryRepository;
        private readonly IMapper _mapper;

        public GetAllStationQueryHandler(IStationQueryRepository stationQueryRepository, IMapper mapper)
        {
            _stationQueryRepository = stationQueryRepository;
            _mapper = mapper;
        }

        public async Task<PaginatedListResponseDTO<StationResponseDTO>> Handle(GetAllStationQuery request, CancellationToken cancellationToken)
        {
            var (count, list) = await _stationQueryRepository.GetAllAsync(request);
            var stationList = _mapper.Map<IEnumerable<StationResponseDTO>>(list);
            return new PaginatedListResponseDTO<StationResponseDTO>(stationList, count, request.PageNumber, request.PageSize);
        }
    }
}
