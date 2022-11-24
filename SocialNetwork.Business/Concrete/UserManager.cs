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
        //get with jwt token
        // public IDataResult<UserByIdDTO> GetUserId()
        // {
        //     try
        //     {   
        //         var user = 12;
        //         if (user != null)
        //         {
        //             var model = _mapper.Map<UserByIdDTO>(user);
        //             return new SuccessDataResult<UserByIdDTO>(model);
        //         }
        //         else
        //         {
        //             return new ErrorDataResult<UserByIdDTO>(Messages.UserNotFound);
        //         }
        //     }
        //     catch (Exception e)
        //     {
        //         throw;
        //     }
        // }
    }
}