using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SocialNetwork.Entities.DTOs
{
    public class PostDTO
    {
        public record SharePostDTO(string content,IFormFile? photoUrl);
        public record ReactPostDTO(int postId);
        public record UserPostListDTO(int postId, string content, int likeCount);
        public record DeletePostDTO(int postId);
    }
}