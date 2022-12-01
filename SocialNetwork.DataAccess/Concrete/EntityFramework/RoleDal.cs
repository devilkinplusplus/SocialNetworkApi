using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SocialNetwork.Core.DataAccess.EntityFramework;
using SocialNetwork.Core.Entities.Concrete;
using SocialNetwork.DataAccess.Abstract;
using SocialNetwork.DataAccess.Concrete;

namespace SocialNetwork.DataAccess.Concrete.EntityFramework
{
    public class RoleDal : EfRepositoryBase<Role, AppDbContext>, IRoleDal
    {

    }
}