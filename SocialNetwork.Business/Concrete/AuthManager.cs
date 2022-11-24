using AutoMapper;
using SocialNetwork.Business.Abstract;
using SocialNetwork.Business.Constants;
using SocialNetwork.Core.Entities.Concrete;
using SocialNetwork.Core.Helpers.Result.Abstract;
using SocialNetwork.Core.Helpers.Result.Concrete.ErrorResults;
using SocialNetwork.Core.Helpers.Result.Concrete.SuccessResults;
using SocialNetwork.Core.Security.Hashing;
using SocialNetwork.Core.Security.Jwt;
using SocialNetwork.DataAccess.Abstract;
using SocialNetwork.Entities.DTOs;
using static SocialNetwork.Entities.DTOs.UserDTO;

namespace SocialNetwork.Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly IUserDal _userDal;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public AuthManager(IUserDal userDal, IMapper mapper, IUserService userService)
        {
            _userDal = userDal;
            _mapper = mapper;
            _userService = userService;
        }

        public IDataResult<User> GetUserByEmail(string email)
        {
            try
            {
                var result = _userDal.Get(x => x.Email == email);
                return new SuccessDataResult<User>(result);
            }
            catch (Exception)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }
        }

        public IResult Login(LoginDTO login)
        {
            try
            {
                var findUserByEmail = GetUserByEmail(login.Email);
                if (!findUserByEmail.Success)
                    return new ErrorResult(Messages.LoginError);

                var checkPassword = HashingHelper.VerifyPassword(login.Password, findUserByEmail.Data.PasswordHash, findUserByEmail.Data.PasswordSalt);
                if (!checkPassword)
                    return new ErrorResult(Messages.LoginError);

                string token = TokenGenerator.Token(findUserByEmail.Data, "User");

                return new SuccessResult(token);
            }
            catch (Exception e)
            {
                return new ErrorResult(e.Message);
            }
        }

        public IResult Register(RegisterDTO register)
        {
            try
            {
                var findUserByEmail = _userService.GetUserByEmail(register.Email);
                if (findUserByEmail.Success)
                    return new ErrorResult(Messages.UserExists);
                byte[] passwordHash, passwordSalt;
                var model = _mapper.Map<User>(register);
                HashingHelper.HashPassword(register.Password, out passwordHash, out passwordSalt);
                model.PasswordHash = passwordHash;
                model.PasswordSalt = passwordSalt;
                model.ProfilePicture = "/";
                _userDal.Add(model);
                return new SuccessResult(Messages.RegisterSuccessfully);
            }
            catch (Exception e)
            {
                return new ErrorResult(e.Message);
            }
        }
    }
}