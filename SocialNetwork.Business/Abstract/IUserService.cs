using SocialNetwork.Core.Helpers.Result.Abstract;
using static SocialNetwork.Entities.DTOs.UserDTO;

namespace SocialNetwork.Business.Abstract
{
    public interface IUserService
    {
        IDataResult<UserByEmailDTO> GetUserByEmail(string email);
    }
}