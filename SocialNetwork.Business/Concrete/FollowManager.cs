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
using static SocialNetwork.Entities.DTOs.FollowDTO;

namespace SocialNetwork.Business.Concrete
{
    public class FollowManager : IFollowService
    {
        private readonly IFollowDal _followDal;
        private readonly IMapper _mapper;
        private readonly IUserDal _userDal;
        public FollowManager(IFollowDal followDal, IMapper mapper, IUserDal userDal)
        {
            _followDal = followDal;
            _mapper = mapper;
            _userDal = userDal;
        }

        public IResult StartFollowing(StartFollowingDTO model, Guid userId)
        {
            try
            {
                var mapper = _mapper.Map<Follow>(model);

                var followingUser = _userDal.Get(x => x.Id == model.followingId);
                if (followingUser.IsPrivate)
                {
                    mapper.HasRequest = true;
                }

                
                mapper.FollowerId = userId;
                mapper.FollowingId = model.followingId;
                mapper.Date = DateTime.Now;
                _followDal.Add(mapper);

                return new SuccessResult(Messages.SuccessMessage);
            }
            catch (Exception e)
            {
                return new ErrorResult(e.Message);
            }
        }
    }
}