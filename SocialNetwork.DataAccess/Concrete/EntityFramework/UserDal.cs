using SocialNetwork.Core.DataAccess.EntityFramework;
using SocialNetwork.Core.Entities.Concrete;
using SocialNetwork.DataAccess.Abstract;

namespace SocialNetwork.DataAccess.Concrete
{
    public class UserDal : EfRepositoryBase<User,AppDbContext>, IUserDal
    {

    }
}