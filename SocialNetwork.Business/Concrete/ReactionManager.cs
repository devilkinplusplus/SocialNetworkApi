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
using static SocialNetwork.Entities.DTOs.PostDTO;

namespace SocialNetwork.Business.Concrete
{
    public class ReactionManager : IReactionService
    {
        private readonly IReactionDal _reactionDal;
        private readonly IMapper _mapper;
        private readonly AppDbContext _appdbContext = new();
        public ReactionManager(IReactionDal reactionDal, IMapper mapper)
        {
            _reactionDal = reactionDal;
            _mapper = mapper;
        }

        public IDataResult<List<Reaction>> DisLikedPosts(Guid userId)
        {
            try
            {
                var posts = _reactionDal.GetAll();
                var dislikedPosts = _appdbContext.Reactions.Where(x => x.IsLike == false && x.UserId == userId).ToList();
                if (dislikedPosts.Count > 0)
                {
                    return new SuccessDataResult<List<Reaction>>(dislikedPosts, Messages.CountOfPosts);
                }
                return new ErrorDataResult<List<Reaction>>(dislikedPosts, Messages.ZeroLikedPost);
            }
            catch (Exception e)
            {
                throw new NullReferenceException();
            }
        }

        public IResult Like(LikePostDTO like, Guid userId)
        {
            try
            {
                var model = _mapper.Map<Reaction>(like);
                model.UserId = userId;
                model.PostId = like.postId;
                model.IsLike = true;
                _reactionDal.Add(model);
                return new SuccessResult(Messages.LikeThePost);
            }
            catch (Exception e)
            {
                return new ErrorResult(e.Message);
            }
        }

        public IDataResult<List<Reaction>> LikedPosts(Guid userId)
        {
            try
            {
                var posts = _reactionDal.GetAll();
                var likedPosts = _appdbContext.Reactions.Where(x => x.IsLike == true && x.UserId == userId).ToList();
                if (likedPosts.Count > 0)
                {
                    return new SuccessDataResult<List<Reaction>>(likedPosts, Messages.CountOfPosts);
                }
                return new ErrorDataResult<List<Reaction>>(likedPosts, Messages.ZeroLikedPost);
            }
            catch (Exception e)
            {
                throw new NullReferenceException();
            }
        }
    }
}