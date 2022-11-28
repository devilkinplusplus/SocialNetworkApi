using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SocialNetwork.Core.Helpers.Result.Abstract;
using SocialNetwork.Entities.Concrete;
using static SocialNetwork.Entities.DTOs.PostDTO;

namespace SocialNetwork.Business.Abstract
{
    public interface IPostService
    {
        IResult Share(SharePostDTO post, Guid userId);
        IDataResult<Post> Delete(int id, Guid userId);
        IDataResult<List<Post>> GetPostsByUser(Guid userId);
    }
}