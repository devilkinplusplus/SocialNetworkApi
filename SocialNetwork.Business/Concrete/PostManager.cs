using System.Security.Claims;
using AutoMapper;
using SocialNetwork.Business.Abstract;
using SocialNetwork.Business.Constants;
using SocialNetwork.Core.Entities.Concrete;
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


        public PostManager(IPostDal postDal, IMapper mapper)
        {
            _postDal = postDal;
            _mapper = mapper;
        }

        public IDataResult<Post> Delete(int id, Guid userId)
        {
            try
            {
                using var _appdbContext = new AppDbContext();
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

        public IDataResult<IEnumerable<Post>> GetAllPosts()
        {
            try
            {
                var data = _postDal.GetAll(x => x.IsDeleted == false);
                if (data != null)
                {
                    return new SuccessDataResult<IEnumerable<Post>>(data);
                }
                return new ErrorDataResult<IEnumerable<Post>>(data, Messages.PostNotFound);
            }
            catch (Exception e)
            {
                return new ErrorDataResult<IEnumerable<Post>>(e.Message);
            }
        }

        public IDataResult<List<Post>> GetPostsByUser(Guid userId)
        {
            try
            {
                using var _appdbContext = new AppDbContext();
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
                    model.PhotoUrl = (post.photoUrl == null) ? null : post.photoUrl.FileName;
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

        public IDataResult<List<Post>> Suggestions(Guid userId)
        {
            try
            {
                var suggestionPosts = _postDal.GetSuggestionPosts(userId);
                return new SuccessDataResult<List<Post>>(suggestionPosts);

            }
            catch (Exception e)
            {
                return new ErrorDataResult<List<Post>>(e.Message);
            }
        }
    }
}