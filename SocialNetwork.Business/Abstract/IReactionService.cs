using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SocialNetwork.Core.Helpers.Result.Abstract;
using SocialNetwork.Entities.Concrete;
using static SocialNetwork.Entities.DTOs.PostDTO;

namespace SocialNetwork.Business.Abstract
{
    public interface IReactionService
    {
        IResult Like(ReactPostDTO like, Guid userId);
        IResult DisLike(ReactPostDTO dislike, Guid userId);
        IDataResult<List<Reaction>> LikedPosts(Guid userId);
        IDataResult<List<Reaction>> DisLikedPosts(Guid userId);
    }
}