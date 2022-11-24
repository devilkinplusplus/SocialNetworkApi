using AutoMapper;
using SocialNetwork.Core.Entities.Concrete;
using SocialNetwork.Entities.Concrete;
using static SocialNetwork.Entities.DTOs.PostDTO;
using static SocialNetwork.Entities.DTOs.UserDTO;

namespace SocialNetwork.Business.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterDTO, User>();
            CreateMap<User, RegisterDTO>();

            CreateMap<LoginDTO, User>();
            CreateMap<User, LoginDTO>();

            CreateMap<UserByEmailDTO, User>();
            CreateMap<User, UserByEmailDTO>();

            CreateMap<UserByIdDTO, User>();
            CreateMap<User, UserByIdDTO>();

            CreateMap<SharePostDTO,Post>();
            CreateMap<Post,SharePostDTO>();
        }
    }
}