using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SocialNetwork.Core.DataAccess.EntityFramework;
using SocialNetwork.DataAccess.Abstract;
using SocialNetwork.Entities.Concrete;

namespace SocialNetwork.DataAccess.Concrete.EntityFramework
{
    public class CommentDal : EfRepositoryBase<Comment, AppDbContext>, ICommentDal
    {
    }
}