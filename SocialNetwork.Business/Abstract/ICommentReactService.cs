using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SocialNetwork.Core.Helpers.Result.Abstract;
using static SocialNetwork.Entities.DTOs.CommentDTO;

namespace SocialNetwork.Business.Abstract
{
    public interface ICommentReactService
    {
        IResult ReactComment(ReactCommentDTO react,Guid userId);
    }
}