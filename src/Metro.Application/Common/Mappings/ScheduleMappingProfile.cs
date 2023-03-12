using AutoMapper;
using Metro.Core.Entities;
using Shared.Commands.Schedules;
using Shared.DTOs;

namespace Metro.Application.Common.Mappings
{
    public class ScheduleMappingProfile : Profile
    {
        public ScheduleMappingProfile()
        {
            //DTO to Domain
            CreateMap<Schedule, ScheduleResponseDTO>().ReverseMap();

            //Command to Domain
            CreateMap<Schedule, CreateScheduleCommand>().ReverseMap();
        }
    }
}
