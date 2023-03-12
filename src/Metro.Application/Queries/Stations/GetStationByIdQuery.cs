using AutoMapper;
using MediatR;
using Metro.Application.Common.Exceptions;
using Metro.Application.Contracts.Repositories.Query;
using Metro.Core.Entities;
using Shared.DTOs;

namespace Metro.Application.Queries.Stations
{
    public class GetStationByIdQuery : IRequest<StationResponseDTO>
    {
        public GetStationByIdQuery(Guid id) 
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }

    public class GetStationByIdQueryHandler : IRequestHandler<GetStationByIdQuery, StationResponseDTO> 
    {
        private readonly IStationQueryRepository _stationQueryRepository;
        private readonly IMapper _mapper;

        public GetStationByIdQueryHandler(IStationQueryRepository stationQueryRepository, IMapper mapper)
        {
            _stationQueryRepository = stationQueryRepository;
            _mapper = mapper;
        }

        public async Task<StationResponseDTO> Handle(GetStationByIdQuery request, CancellationToken cancellationToken)
        {
            var station = await _stationQueryRepository.GetByIdAsync(request.Id);
            if(station == null)
            {
                throw new NotFoundException("Station data not found");
            }
            return _mapper.Map<Station, StationResponseDTO>(station);
        }
    }
}
