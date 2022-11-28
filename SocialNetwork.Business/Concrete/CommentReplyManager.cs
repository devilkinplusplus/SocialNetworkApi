using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SocialNetwork.Business.Abstract;
using SocialNetwork.Business.Constants;
using SocialNetwork.Core.Helpers.Result.Abstract;
using SocialNetwork.Core.Helpers.Result.Concrete.ErrorResults;
using SocialNetwork.Core.Helpers.Result.Concrete.SuccessResults;
using SocialNetwork.DataAccess.Abstract;
using SocialNetwork.Entities.Concrete;
using SocialNetwork.Entities.DTOs;
using static SocialNetwork.Entities.DTOs.CommentDTO;

namespace SocialNetwork.Business.Concrete
{
    public class CommentReplyManager : ICommentReplyService
    {
        private readonly ICommentReplyDal _replyDal;
        private readonly IMapper _mapper;
        public CommentReplyManager(ICommentReplyDal replyDal, IMapper mapper)
        {
            _replyDal = replyDal;
            _mapper = mapper;
        }

        public IResult ReplyComment(ReplyCommentDTO reply, Guid userId)
        {
            try
            {
                if (reply.content != null && reply.content != string.Empty)
                {
                    var mapper = _mapper.Map<CommentReply>(reply);
                    mapper.UserId = userId;
                    mapper.Content = reply.content;
                    mapper.PublishDate = DateTime.Now;
                    mapper.CommentId = reply.commentId;
                    _replyDal.Add(mapper);
                    return new SuccessResult(Messages.CommentShared);
                }
                else
                {
                    return new ErrorResult(Messages.NullReference);
                }
            }
            catch (Exception e)
            {
                return new ErrorResult(e.Message);
            }
        }
    }
}