using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Entities.DTOs
{
    public class PostDTO
    {
        public record SharePostDTO(string content);
    }
}