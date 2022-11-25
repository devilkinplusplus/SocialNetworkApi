using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SocialNetwork.Core.DataAccess;
using SocialNetwork.Entities.Concrete;

namespace SocialNetwork.DataAccess.Abstract
{
    public interface IPostDal : IRepositoryBase<Post>
    {
        
    }
}