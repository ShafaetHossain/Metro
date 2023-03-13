using AutoMapper;
using Metro.Core.Entities;
using Shared.Commands.TicketInfoes;
using Shared.DTOs;

namespace Metro.Application.Common.Mappings
{
    public class TicketInfoMappingProfile : Profile
    {
        public TicketInfoMappingProfile() 
        {
            //DTO to Domain
            CreateMap<TicketInfo, TicketInfoResponseDTO>();

            //Command to Domain
            CreateMap<TicketInfo, CreateTicketInfoCommand>();
        }
    }
}
