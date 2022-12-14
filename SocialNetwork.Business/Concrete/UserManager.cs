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
using SocialNetwork.Core.Security.Hashing;
using SocialNetwork.Business.Validators;
using FluentValidation.Results;

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

        public IDataResult<IEnumerable<User>> GetAllUsers()
        {
            try
            {
                var data = _userDal.GetAll();
                if(data!=null){
                    return new SuccessDataResult<IEnumerable<User>>(data);
                }
                return new ErrorDataResult<IEnumerable<User>>(data,Messages.UserNotFound);
            }
            catch (Exception e)
            {
                return new ErrorDataResult<IEnumerable<User>>(e.Message);
            }
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

        public IResult UpdateUser(UpdateUserDTO model, Guid userId)
        {
            try
            {
                var mapper = _mapper.Map<User>(model);
                var currentUser = _userDal.Get(x => x.Id == userId);
                if (currentUser != null)
                {
                    currentUser.Name = model.Name;
                    currentUser.Surname = model.Surname;
                    currentUser.UserName = model.UserName;
                    currentUser.BirthDay = model.BirthDay;
                    currentUser.IsPrivate = model.IsPrivate;
                    currentUser.ProfilePicture = (model.PhotoUrl == null) ? null : model.PhotoUrl.FileName;
                    byte[] passwordSalt, passwordHash;
                    HashingHelper.HashPassword(model.Password, out passwordHash, out passwordSalt);
                    currentUser.PasswordHash = passwordHash;
                    currentUser.PasswordSalt = passwordSalt;

                    UserValidator validationRules = new UserValidator();
                    ValidationResult result = validationRules.Validate(currentUser);
                    if (result.IsValid)
                    {
                        _userDal.Update(currentUser);
                        return new SuccessResult(Messages.UpdateMessage);
                    }
                }

                return new ErrorResult(Messages.UserNotFound);

            }
            catch (Exception e)
            {
                return new ErrorResult(e.Message);
            }
        }
    }
}