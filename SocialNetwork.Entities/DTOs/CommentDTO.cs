using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Entities.DTOs
{
    public class CommentDTO
    {
        public record ShareCommentDTO(string content, int postId);
    }
}