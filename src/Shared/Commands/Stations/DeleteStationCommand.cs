using MediatR;

namespace Shared.Commands.Stations
{
    public record DeleteStationCommand(Guid Id) : IRequest<string>
    {
    }
}
