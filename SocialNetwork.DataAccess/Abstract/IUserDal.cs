using SocialNetwork.Core.DataAccess;
using SocialNetwork.Core.Entities.Concrete;
using static SocialNetwork.Entities.DTOs.PostDTO;
using static SocialNetwork.Entities.DTOs.UserDTO;

namespace SocialNetwork.DataAccess.Abstract
{
    public interface IUserDal : IRepositoryBase<User>
    {
        IEnumerable<UserPostListDTO> GetUserPostList(Guid userId);
        IEnumerable<User> GetMostActiveUsers();
    }
}