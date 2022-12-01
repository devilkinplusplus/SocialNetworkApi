using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SocialNetwork.Business.Abstract;
using SocialNetwork.Business.Constants;
using SocialNetwork.Core.Entities.Concrete;
using SocialNetwork.Core.Helpers.Result.Abstract;
using SocialNetwork.Core.Helpers.Result.Concrete.ErrorResults;
using SocialNetwork.Core.Helpers.Result.Concrete.SuccessResults;
using SocialNetwork.DataAccess.Abstract;
using SocialNetwork.DataAccess.Concrete;
using SocialNetwork.Entities.DTOs;
using static SocialNetwork.Entities.DTOs.RoleDTO;

namespace SocialNetwork.Business.Concrete
{
    public class UserRoleManager : IUserRoleService
    {
        private readonly IUserRoleDal _userRoleDal;
        private readonly IMapper _mapper;
        public UserRoleManager(IUserRoleDal userRoleDal, IMapper mapper)
        {
            _userRoleDal = userRoleDal;
            _mapper = mapper;
        }

        public IResult AddRole(string roleName, Guid userId)
        {
            try
            {
                using var context = new AppDbContext();
                var currentRole = context.Roles.FirstOrDefault(x => x.RoleName == roleName);

                if (currentRole != null)
                {
                    context.UserRoles.Add(new UserRole
                    {
                        RoleId = currentRole.Id,
                        UserId = userId
                    });
                    context.SaveChanges();
                    return new SuccessResult();
                }
                else
                {
                    return new ErrorResult(Messages.NullReference);
                }
            }
            catch (Exception e)
            {
                return new ErrorResult(e.Message);
            }
        }
    }
}