using AutoMapper;
using MediatR;
using Metro.Application.Common.Exceptions;
using Metro.Application.Contracts.Repositories;
using Metro.Application.Contracts.Repositories.Command;
using Metro.Application.Contracts.Repositories.Query;
using Metro.Core.Entities;
using Shared.Commands.Stations;
using Shared.DTOs;

namespace Metro.Application.CommandHandlers.Stations
{
    public class UpdateStationCommandHandler : IRequestHandler<UpdateStationCommand, StationResponseDTO>
    {
        private readonly IStationCommandRepository _stationCommandRepository;
        private readonly IStationQueryRepository _stationQueryRepository;

        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateStationCommandHandler(IStationCommandRepository stationCommandRepository, IStationQueryRepository stationQueryRepository,
            IMediator mediator, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _stationCommandRepository = stationCommandRepository;
            _stationQueryRepository = stationQueryRepository;
            _mediator = mediator;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<StationResponseDTO> Handle(UpdateStationCommand request, CancellationToken cancellationToken)
        {
            var currentStation = await _stationQueryRepository.GetByIdAsync(request.Id);
            if(currentStation == null)
            {
                throw new NotFoundException("Station data not found");
            }
            //map request type to entity(table) type so that we can update
            var stationEntity = _mapper.Map<UpdateStationCommand, Station>(request, currentStation);

            //update the entity and collect the response
            var updateStationEntity = _stationCommandRepository.UpdateAsync(stationEntity);

            //commit changes
            await _unitOfWork.CommitAsync(cancellationToken);

            return _mapper.Map<StationResponseDTO>(updateStationEntity.Result);
        }
    }
}
