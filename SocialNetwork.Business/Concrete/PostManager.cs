using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Http;
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
    public class PostManager : IPostService
    {
        private readonly IPostDal _postDal;
        private readonly IMapper _mapper;
        private readonly AppDbContext _appdbContext = new();

        public PostManager(IPostDal postDal, IMapper mapper)
        {
            _postDal = postDal;
            _mapper = mapper;
        }

        public IDataResult<Post> Delete(int id, Guid userId)
        {
            try
            {
                var checkedPost = _appdbContext.Posts.
                Where(x => x.Id == id && x.UserId == userId).FirstOrDefault();

                if (checkedPost != null)
                {
                    checkedPost.IsDeleted = true;
                    _postDal.Update(checkedPost);
                    return new SuccessDataResult<Post>(checkedPost);
                }
                else
                {
                    return new ErrorDataResult<Post>(Messages.NullReference);
                }

            }
            catch (Exception e)
            {
                return new ErrorDataResult<Post>(e.Message);
            }
        }

        public IDataResult<List<Post>> GetPostsByUser(Guid userId)
        {
            try
            {
                var posts = _appdbContext.Posts.
                Where(x => x.UserId == userId && x.IsDeleted == false).ToList();
                if (posts != null)
                {
                    return new SuccessDataResult<List<Post>>(posts);
                }
                else
                {
                    return new ErrorDataResult<List<Post>>(Messages.PostNotFound);
                }
            }
            catch (Exception e)
            {
                return new ErrorDataResult<List<Post>>(e.Message);
            }
        }

        public IResult Share(SharePostDTO post, Guid userId)
        {
            try
            {
                if (post.content != null)
                {
                    var model = _mapper.Map<Post>(post);
                    model.UserId = userId;
                    model.PublishDate = DateTime.Now;
                    _postDal.Add(model);
                    return new SuccessResult(Messages.PostSuccess);
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