using AutoMapper;
using Metro.Core.Entities;
using Shared.Commands.Users;
using Shared.DTOs;

namespace Metro.Application.Common.Mappings
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile() 
        {
            //DTO to Domain
            CreateMap<User, UserResponseDTO>().ReverseMap();

            //Command to Domain
            CreateMap<User, CreateUserCommand>().ReverseMap();
        }
    }
}
