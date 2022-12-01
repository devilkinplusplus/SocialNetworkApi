using SocialNetwork.Core.Helpers.Result.Abstract;
using static SocialNetwork.Entities.DTOs.PostDTO;
using static SocialNetwork.Entities.DTOs.UserDTO;

namespace SocialNetwork.Business.Abstract
{
    public interface IUserService
    {
        IDataResult<UserByEmailDTO> GetUserByEmail(string email);
        IDataResult<IEnumerable<UserPostListDTO>> GetUserPosts(Guid userId);
        IResult UpdateUser(UpdateUserDTO model, Guid userId);
    }
}