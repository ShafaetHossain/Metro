using AutoMapper;
using MediatR;
using Metro.Application.Contracts.Repositories;
using Metro.Application.Contracts.Repositories.Command;
using Metro.Core.Entities;
using Shared.Commands.Stations;
using Shared.DTOs;

namespace Metro.Application.CommandHandlers.Stations
{
    public class CreateStationCommandHandler : IRequestHandler<CreateStationCommand, StationResponseDTO>
    {
        private readonly IMapper _mapper;
        private readonly IStationCommandRepository _stationCommandRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateStationCommandHandler(IMapper mapper, IStationCommandRepository stationCommandRepository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _stationCommandRepository = stationCommandRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<StationResponseDTO> Handle(CreateStationCommand request, CancellationToken cancellationToken)
        {
            //map request type to entity(table) type so that we can insert
            var entity = _mapper.Map<Station>(request);

            //insert the entity and collect the response
            var newEntity = await _stationCommandRepository.InsertAsync(entity);

            //commit changes
            await _unitOfWork.CommitAsync(cancellationToken);

            return _mapper.Map<StationResponseDTO>(request);
        }
    }
}
