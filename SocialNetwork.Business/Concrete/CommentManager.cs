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
    public class CommentManager : ICommentService
    {
        private readonly ICommentDal _commentDal;
        private readonly IMapper _mapper;
        public CommentManager(ICommentDal commentDal, IMapper mapper)
        {
            _commentDal = commentDal;
            _mapper = mapper;
        }

        public IResult DeleteComment(int id, Guid userId)
        {
            try
            {
                var currentComment = _commentDal.Get(x => x.Id == id && x.UserId==userId);
                if (currentComment != null)
                {
                    currentComment.IsDeleted = true;
                    _commentDal.Update(currentComment);
                    return new SuccessDataResult<Comment>(currentComment, Messages.UpdateMessage);
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


        public IResult ShareComment(ShareCommentDTO comment, Guid userId)
        {
            try
            {
                if (comment.content != null)
                {
                    var model = _mapper.Map<Comment>(comment);
                    model.UserId = userId;
                    model.PostId = comment.postId;
                    model.Content = comment.content;
                    model.PublishDate = DateTime.Now;
                    _commentDal.Add(model);
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