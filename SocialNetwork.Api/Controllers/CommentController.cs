using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using SocialNetwork.Business.Abstract;
using static SocialNetwork.Entities.DTOs.CommentDTO;

namespace SocialNetwork.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]/v1")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly IReplyService _replyService;
        private readonly IUserService _userService;
        public CommentController(ICommentService commentService, IUserService userService, IReplyService replyService)
        {
            _commentService = commentService;
            _userService = userService;
            _replyService = replyService;
        }

        [HttpPost("comment")]
        public IActionResult Comment(ShareCommentDTO model)
        {
            var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(_bearer_token);
            var id = jwtSecurityToken.Claims.FirstOrDefault(x => x.Type == "nameid")?.Value;

            var result = _commentService.ShareComment(model, Guid.Parse(id));
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpPut("delete")]
        public IActionResult Delete(int commentId)
        {
            var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(_bearer_token);
            var id = jwtSecurityToken.Claims.FirstOrDefault(x => x.Type == "nameid")?.Value;

            var result = _commentService.DeleteComment(commentId, Guid.Parse(id));
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }


        [HttpPost("replyComment")]
        public IActionResult ReplyComment(ShareCommentDTO model)
        {
            var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(_bearer_token);
            var id = jwtSecurityToken.Claims.FirstOrDefault(x => x.Type == "nameid")?.Value;

            var result = _replyService.ShareComment(model, Guid.Parse(id));
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
    }
}