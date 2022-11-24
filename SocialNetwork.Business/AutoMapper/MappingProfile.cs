using AutoMapper;
using SocialNetwork.Core.Entities.Concrete;
using static SocialNetwork.Entities.DTOs.UserDTO;

namespace SocialNetwork.Business.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterDTO, User>();
            CreateMap<User, RegisterDTO>();

            CreateMap<UserByEmailDTO, User>();
            CreateMap<User, UserByEmailDTO>();
            
        }
    }
}