using Microsoft.EntityFrameworkCore;
using SocialNetwork.Core.DataAccess.EntityFramework;
using SocialNetwork.Core.Entities.Concrete;
using SocialNetwork.DataAccess.Abstract;
using SocialNetwork.Entities.DTOs;
using static SocialNetwork.Entities.DTOs.PostDTO;
using static SocialNetwork.Entities.DTOs.UserDTO;

namespace SocialNetwork.DataAccess.Concrete
{
    public class UserDal : EfRepositoryBase<User, AppDbContext>, IUserDal
    {

        public IEnumerable<User> GetMostActiveUsers()
        {
            using var context = new AppDbContext();
                       List<User> users = new();
          
            return users;
        }

        public IEnumerable<UserPostListDTO> GetUserPostList(Guid userId)
        {
            using var context = new AppDbContext();
            List<UserPostListDTO> results = new();
            var posts = context.Posts.Include(x => x.Reactions).Where(x => x.UserId == userId).ToList();

            foreach (var item in posts)
            {
                int likeCount = item.Reactions.Where(x => x.IsLike == true).Count();
                UserPostListDTO postListDTO = new(item.Id, item.Content, likeCount);
                results.Add(postListDTO);
            }

            return results;
        }


    }
}