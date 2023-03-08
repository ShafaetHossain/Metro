using MediatR;
using Shared.DTOs;

namespace Shared.Commands.Stations
{
    public class CreateStationCommand : IRequest<StationResponseDTO>
    {
        public string StationName { get; set; }
    }
}
