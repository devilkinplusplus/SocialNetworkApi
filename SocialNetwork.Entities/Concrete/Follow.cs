using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using SocialNetwork.Core.Entities;
using SocialNetwork.Core.Entities.Concrete;

namespace SocialNetwork.Entities.Concrete
{
    public class Follow : IEntity
    {
        public int Id { get; set; }
        public Guid FollowerId { get; set; }
        [ForeignKey("FollowerId")]
        public User Follower { get; set; }
        public Guid FollowingId { get; set; }
        [ForeignKey("FollowingId")]
        public User Following { get; set; }
        public DateTime Date { get; set; }
        public bool IsDeleted { get; set; }
        public bool HasRequest { get; set; }
    }
}