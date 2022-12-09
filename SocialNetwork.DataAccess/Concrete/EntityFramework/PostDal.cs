using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SocialNetwork.Core.DataAccess.EntityFramework;
using SocialNetwork.DataAccess.Abstract;
using SocialNetwork.Entities.Concrete;

namespace SocialNetwork.DataAccess.Concrete.EntityFramework
{
    public class PostDal : EfRepositoryBase<Post, AppDbContext>, IPostDal
    {
        public List<Post> GetSuggestionPosts(Guid userId)
        {
            using var _appdbContext = new AppDbContext();
            var users = _appdbContext.Users.Where(x => x.Id != userId && x.IsPrivate == false).
                        Select(x => x.Id).ToList();
            List<Post> suggestionPosts = new();

            if (users.Count == 0)
            {
                var posts = _appdbContext.Posts.Where(x => x.IsDeleted == false && x.User.IsPrivate == false).
                                                ToList();
                suggestionPosts.AddRange(posts);
            }

            for (int i = 0; i < users.Count; i++)
            {
                var posts = _appdbContext.Posts.Where(x => x.UserId == users[i] && x.IsDeleted == false);
                foreach (var item in posts)
                {
                    if (!suggestionPosts.Contains(item))
                        suggestionPosts.Add(item);
                }
            }
            if (suggestionPosts.Count == 0)
            {
                var posts = _appdbContext.Posts.Where(x => x.IsDeleted == false && x.User.IsPrivate == false).ToList();
                suggestionPosts.AddRange(posts);
            }
            return suggestionPosts;
        }
    }
}