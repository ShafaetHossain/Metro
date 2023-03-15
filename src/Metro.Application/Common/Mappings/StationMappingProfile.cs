using AutoMapper;
using Metro.Core.Entities;
using Shared.Commands.Stations;
using Shared.DTOs;

namespace Metro.Application.Common.Mappings
{
    public class StationMappingProfile : Profile
    {
        public StationMappingProfile() 
        {
            //DTO to Domain
            CreateMap<Station, StationResponseDTO>().ReverseMap();

            //Command to Domain
            CreateMap<Station, CreateStationCommand>().ReverseMap();
            CreateMap<Station, UpdateStationCommand>().ReverseMap();
            CreateMap<Station, DeleteStationCommand>().ReverseMap();
        }
    }
}
