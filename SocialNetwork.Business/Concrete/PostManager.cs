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
                using var _appdbContext = new AppDbContext();
                var followingUsers = _appdbContext.Follows
                                    .Where(x => x.FollowerId != userId && x.Following.IsPrivate == false)
                                    .Select(x => x.FollowingId).ToList();

                List<Post> suggestionPosts = new();

                if (followingUsers.Count == 0)
                {
                    var posts = _appdbContext.Posts.Where(x => x.IsDeleted == false).ToList();
                    suggestionPosts.AddRange(posts);
                }

                for (int i = 0; i < followingUsers.Count; i++)
                {
                    var post = _appdbContext.Posts.Where(x => x.UserId != followingUsers[i] && x.IsDeleted == false);
                    foreach (var item in post)
                    {
                        if(!suggestionPosts.Contains(item))
                            suggestionPosts.Add(item);
                    }
                }
                if (suggestionPosts.Count == 0)
                {
                    var posts = _appdbContext.Posts.Where(x => x.IsDeleted == false).ToList();
                    suggestionPosts.AddRange(posts);
                }
                return new SuccessDataResult<List<Post>>(suggestionPosts);

            }
            catch (Exception e)
            {
                return new ErrorDataResult<List<Post>>(e.Message);
            }
        }
    }
}