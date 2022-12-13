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
using SocialNetwork.DataAccess.Concrete;
using SocialNetwork.Entities.DTOs;
using static SocialNetwork.Entities.DTOs.UserDTO;

namespace SocialNetwork.Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly IUserDal _userDal;
        private readonly IRoleDal _roleDal;
        private readonly IUserRoleDal _userRoleDal;
        private readonly IUserService _userService;
        private readonly IUserRoleService _userRoleService;
        private readonly IMapper _mapper;
        public AuthManager(IUserDal userDal, IMapper mapper, IUserService userService, IUserRoleService userRoleService, IRoleDal roleDal, IUserRoleDal userRoleDal)
        {
            _userDal = userDal;
            _mapper = mapper;
            _userService = userService;
            _userRoleService = userRoleService;
            _roleDal = roleDal;
            _userRoleDal = userRoleDal;
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

        private bool IsLockoutEnabled(DateTime time, double additionalTime)
        {
            if (time.AddMinutes(additionalTime) <= DateTime.Now)
                return true;
            return false;
        }

        private bool IsAdminSignin(Guid userId)
        {
            using var context = new AppDbContext();
            var roleAdminId = context.Roles.Where(x => x.RoleName == "Admin").Select(x => x.Id).FirstOrDefault();
            var admins = context.UserRoles.Where(x => x.RoleId == roleAdminId).Select(x => x.UserId).ToList();

            foreach (var item in admins)
            {
                if (admins.Contains(userId))
                {
                    return true;
                }
            }
            return false;
        }

        public IResult Login(LoginDTO login)
        {
            try
            {
                var findUserByEmail = GetUserByEmail(login.Email);

                if (IsLockoutEnabled(findUserByEmail.Data.LoginFailedTime.GetValueOrDefault(), 1))
                {
                    if (!findUserByEmail.Success)
                        return new ErrorResult(Messages.LoginError);

                    var checkPassword = HashingHelper.VerifyPassword(login.Password, findUserByEmail.Data.PasswordHash, findUserByEmail.Data.PasswordSalt);
                    if (!checkPassword)
                    {
                        findUserByEmail.Data.FailedLogin++;
                        if (findUserByEmail.Data.FailedLogin == 5)
                        {
                            findUserByEmail.Data.FailedLogin = 0;
                            findUserByEmail.Data.LoginFailedTime = DateTime.Now;
                        }
                        _userDal.Update(findUserByEmail.Data);
                        return new ErrorResult(Messages.LoginError);
                    }
                    if (findUserByEmail.Data.FailedLogin != 0)
                    {
                        findUserByEmail.Data.FailedLogin = 0;
                        _userDal.Update(findUserByEmail.Data);
                    }

                    string roleName = string.Empty;
                    if (IsAdminSignin(findUserByEmail.Data.Id))
                        roleName = "Admin";
                    else
                        roleName = "User";

                    string token = TokenGenerator.Token(findUserByEmail.Data, roleName);

                    return new SuccessResult(token);
                }
                else
                {
                    return new ErrorResult(Messages.LoginFailed);
                }
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
                _userRoleService.AddRole("User", model.Id);
                return new SuccessResult(Messages.RegisterSuccessfully);
            }
            catch (Exception e)
            {
                return new ErrorResult(e.Message);
            }
        }
    }
}