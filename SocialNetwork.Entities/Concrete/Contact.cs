using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SocialNetwork.Core.Entities;
using SocialNetwork.Core.Entities.Concrete;

namespace SocialNetwork.Entities.Concrete
{
    public class Contact:IEntity
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public string Message { get; set; }
        public DateTime SentDate { get; set; }
    }
}