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
using SocialNetwork.DataAccess.Concrete;
using SocialNetwork.Entities.Concrete;
using SocialNetwork.Entities.DTOs;
using static SocialNetwork.Entities.DTOs.CommentDTO;

namespace SocialNetwork.Business.Concrete
{
    public class ReplyManager : IReplyService
    {
        private readonly IReplyDal _replyDal;
        private readonly ICommentDal _commentDal;
        private readonly IMapper _mapper;
        public ReplyManager(IReplyDal replyDal, IMapper mapper, ICommentDal commentDal)
        {
            _replyDal = replyDal;
            _mapper = mapper;
            _commentDal = commentDal;
        }

        public IResult ShareComment(ShareCommentDTO comment, Guid userId)
        {
            try
            {
                using var context = new AppDbContext();

                if (comment.content != null)
                {
                    var model = _mapper.Map<Comment>(comment);
                    model.UserId = userId;
                    model.PostId = comment.postId;
                    model.Content = comment.content;
                    model.PublishDate = DateTime.Now;
                    _commentDal.Add(model);
                    
                    context.Replies.Add(new Reply
                    {
                        UserId = userId,
                        CommentId = model.Id
                    });
                    context.SaveChanges();

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