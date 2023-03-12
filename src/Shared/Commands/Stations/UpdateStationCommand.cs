using MediatR;
using Shared.DTOs;

namespace Shared.Commands.Stations
{
    public class UpdateStationCommand : IRequest<StationResponseDTO>
    {
        public Guid Id {  get; set; }
        public string StationName { get; set; } 
    }
}
