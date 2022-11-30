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
        public CommentReactManager(ICommentReactionDal commentReactDal, IMapper mapper)
        {
            _commentReactDal = commentReactDal;
            _mapper = mapper;
        }

        public IResult ReactComment(int commentId, Guid userId)
        {
            try
            {
                _commentReactDal.CommentLike(commentId, userId);
                return new SuccessResult(Messages.CommentLiked);
            }
            catch (Exception e)
            {
                return new ErrorResult(e.Message);
            }
        }
    }
}