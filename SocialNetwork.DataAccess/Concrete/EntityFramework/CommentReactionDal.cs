using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SocialNetwork.Core.DataAccess.EntityFramework;
using SocialNetwork.DataAccess.Abstract;
using SocialNetwork.Entities.Concrete;

namespace SocialNetwork.DataAccess.Concrete.EntityFramework
{
    public class CommentReactionDal : EfRepositoryBase<CommentReaction, AppDbContext>, ICommentReactionDal
    {
        public void CommentLike(int commentId, Guid userId)
        {
            using var context = new AppDbContext();
            var commentLike = context.CommentReactions.
            FirstOrDefault(x => x.UserId == userId && x.CommentId == commentId);

            if (commentLike == null)
            {
                context.CommentReactions.Add(new CommentReaction
                {
                    UserId = userId,
                    CommentId = commentId,
                    IsLike = true
                });
            }
            else
            {
                context.CommentReactions.Remove(commentLike);
            }
            context.SaveChanges();
        }
    }
}