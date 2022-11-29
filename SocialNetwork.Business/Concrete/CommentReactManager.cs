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
using SocialNetwork.DataAccess.Concrete.EntityFramework;
using SocialNetwork.Entities.Concrete;
using SocialNetwork.Entities.DTOs;
using static SocialNetwork.Entities.DTOs.CommentDTO;

namespace SocialNetwork.Business.Concrete
{
    public class CommentReactManager : ICommentReactService
    {
        private readonly ICommentReactionDal _commentReactDal;
        private readonly IMapper _mapper;
        private readonly AppDbContext _appDbContext = new();
        public CommentReactManager(ICommentReactionDal commentReactDal, IMapper mapper)
        {
            _commentReactDal = commentReactDal;
            _mapper = mapper;
        }

        private void ReactStatus(ReactCommentDTO react, Guid userId,bool status)
        {
            var model = _mapper.Map<CommentReaction>(react);
            model.UserId = userId;
            model.CommentId = react.commentId;

            if (model.IsLike != status)
            {
                model.IsLike = status;
                _commentReactDal.Add(model);
            }
            else if (model.IsLike == status)
            {
                _commentReactDal.Delete(model);
            }
        }

        public IResult ReactComment(ReactCommentDTO react, Guid userId)
        {
            try
            {
                var mapper = _mapper.Map<CommentReaction>(react);
                mapper.UserId = userId;
                mapper.CommentId = react.commentId;
                

                if (mapper != null)
                {
                    ReactStatus(react,userId,true);
                    return new SuccessResult(Messages.CommentLiked);
                }
                return new ErrorResult(Messages.NullReference);

            }
            catch (Exception e)
            {
                return new ErrorResult(e.Message);
            }
        }
    }
}