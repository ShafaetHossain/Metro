using AutoMapper;
using MediatR;
using Metro.Application.Common.Constants;
using Metro.Application.Common.Exceptions;
using Metro.Application.Contracts.Repositories;
using Metro.Application.Contracts.Repositories.Command;
using Metro.Application.Contracts.Repositories.Query;
using Shared.Commands.Stations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metro.Application.CommandHandlers.Stations
{
    public class DeleteStationCommandHandler : IRequestHandler<DeleteStationCommand, string>
    {
        private readonly IStationCommandRepository _stationCommandRepository;
        private readonly IStationQueryRepository _stationQueryRepositor;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteStationCommandHandler(IStationCommandRepository stationCommandRepository, IStationQueryRepository stationQueryRepositor, IUnitOfWork unitOfWork)
        {
            _stationCommandRepository = stationCommandRepository;
            _stationQueryRepositor = stationQueryRepositor;
            _unitOfWork= unitOfWork;
        }

        public async Task<string> Handle(DeleteStationCommand request, CancellationToken cancellationToken)
        {
            var stationEntity = await _stationQueryRepositor.GetByIdAsync(request.Id);
            if(stationEntity == null)
            {
                throw new NotFoundException(ValidationError.StationNotFound);
            }

            stationEntity.IsDeleted = true;

            await _stationCommandRepository.UpdateAsync(stationEntity);

            await _unitOfWork.CommitAsync(cancellationToken);

            return ValidationError.StationDeleted;
        }
    }
}
