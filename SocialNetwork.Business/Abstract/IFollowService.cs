using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SocialNetwork.Core.Helpers.Result.Abstract;
using static SocialNetwork.Entities.DTOs.FollowDTO;

namespace SocialNetwork.Business.Abstract
{
    public interface IFollowService
    {
        IResult StartFollowing(StartFollowingDTO model,Guid userId);
    }
}