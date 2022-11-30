using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using SocialNetwork.Business.Abstract;
using SocialNetwork.Business.Constants;
using SocialNetwork.Core.Entities.Concrete;
using SocialNetwork.Core.Helpers.Result.Abstract;
using SocialNetwork.Core.Helpers.Result.Concrete.ErrorResults;
using SocialNetwork.Core.Helpers.Result.Concrete.SuccessResults;
using SocialNetwork.DataAccess.Abstract;
using Microsoft.AspNetCore.Http;
using static SocialNetwork.Entities.DTOs.UserDTO;
using SocialNetwork.Entities.DTOs;
using static SocialNetwork.Entities.DTOs.PostDTO;

namespace SocialNetwork.Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;
        private readonly IMapper _mapper;
        public UserManager(IUserDal userDal, IMapper mapper)
        {
            _userDal = userDal;
            _mapper = mapper;
        }

        public IDataResult<UserByEmailDTO> GetUserByEmail(string email)
        {
            try
            {
                var user = _userDal.Get(x => x.Email == email);
                if (user.Email != null)
                {
                    var model = _mapper.Map<UserByEmailDTO>(user);
                    return new SuccessDataResult<UserByEmailDTO>(model);
                }
                else
                {
                    return new ErrorDataResult<UserByEmailDTO>(Messages.UserNotFound);
                }
            }
            catch (Exception e)
            {
                return new ErrorDataResult<UserByEmailDTO>(e.Message);
            }
        }

        public IDataResult<IEnumerable<UserPostListDTO>> GetUserPosts(Guid userId)
        {
            try
            {
                var result = _userDal.GetUserPostList(userId);
                return new SuccessDataResult<IEnumerable<UserPostListDTO>>(result);
            }
            catch (Exception e)
            {
                return new ErrorDataResult<IEnumerable<UserPostListDTO>>(e.Message);
            }
        }
    }
}