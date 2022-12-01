using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SocialNetwork.Core.Entities.Concrete;
using SocialNetwork.Core.Helpers.Result.Abstract;
using static SocialNetwork.Entities.DTOs.RoleDTO;

namespace SocialNetwork.Business.Abstract
{
    public interface IUserRoleService
    {
        IResult AddRole(string roleName, Guid userId);
    }
}