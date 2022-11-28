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
    public class ReplyController : ControllerBase
    {
        private readonly ICommentReplyService _replyService;
        private readonly IUserService _userService;
        public ReplyController(ICommentReplyService replyService, IUserService userService)
        {
            _replyService = replyService;
            _userService = userService;
        }

        [HttpPost("replyComment")]
        public IActionResult ReplyComment(ReplyCommentDTO model)
        {
            var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(_bearer_token);
            var id = jwtSecurityToken.Claims.FirstOrDefault(x => x.Type == "nameid")?.Value;

            var result = _replyService.ReplyComment(model, Guid.Parse(id));
            if (result.Success)
                return Ok(result.Message);
            return BadRequest(result.Message);
        }
    }
}