using MediatR;

namespace Shared.Commands.Schedules
{
    public record DeleteScheduleCommand(Guid Id) : IRequest<string>
    {
    }
}
