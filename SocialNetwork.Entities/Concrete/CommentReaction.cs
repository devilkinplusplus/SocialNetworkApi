using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SocialNetwork.Core.Entities;
using SocialNetwork.Core.Entities.Concrete;

namespace SocialNetwork.Entities.Concrete
{
    public class CommentReaction : IEntity
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public int CommentId { get; set; }
        public Comment Comment { get; set; }
        public bool IsLike { get; set; }

    }
}