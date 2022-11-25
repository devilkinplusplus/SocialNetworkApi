using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using SocialNetwork.Business.Abstract;
using SocialNetwork.Business.Constants;
using SocialNetwork.Core.Helpers.Result.Abstract;
using SocialNetwork.Core.Helpers.Result.Concrete.ErrorResults;
using SocialNetwork.Core.Helpers.Result.Concrete.SuccessResults;
using SocialNetwork.DataAccess.Abstract;
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