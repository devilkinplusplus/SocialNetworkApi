using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SocialNetwork.Core.Helpers.Result.Abstract;
using SocialNetwork.Entities.Concrete;
using static SocialNetwork.Entities.DTOs.CommentDTO;

namespace SocialNetwork.Business.Abstract
{
    public interface IReplyService
    {
        IResult ShareComment(ShareCommentDTO comment, Guid userId);

    }
}