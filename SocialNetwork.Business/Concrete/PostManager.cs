using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SocialNetwork.Business.Abstract;
using SocialNetwork.Core.Helpers.Result.Abstract;
using SocialNetwork.Entities.DTOs;
using static SocialNetwork.Entities.DTOs.PostDTO;

namespace SocialNetwork.Business.Concrete
{
    public class PostManager : IPostService
    {
        public IResult Share(SharePostDTO post)
        {
            throw new NotImplementedException();
        }
    }
}