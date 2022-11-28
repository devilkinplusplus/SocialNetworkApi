using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using SocialNetwork.Business.Abstract;
using static SocialNetwork.Entities.DTOs.PostDTO;

namespace SocialNetwork.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/v1")]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly IUserService _userService;
        public PostController(IPostService postService, IUserService userService)
        {
            _postService = postService;
            _userService = userService;
        }


        [Authorize]
        [HttpPost("share")]
        public IActionResult Share(SharePostDTO model)
        {
            var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(_bearer_token);
            var email = jwtSecurityToken.Claims.FirstOrDefault(x => x.Type == "email")?.Value;

            var user = _userService.GetUserByEmail(email);

            var result = _postService.Share(model, user.Data.Id);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [Authorize]
        [HttpPut("delete")]
        public IActionResult Delete(int postId)
        {
            var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(_bearer_token);
            var email = jwtSecurityToken.Claims.FirstOrDefault(x => x.Type == "email")?.Value;

            var user = _userService.GetUserByEmail(email);

            var result = _postService.Delete(postId, user.Data.Id);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [Authorize]
        [HttpGet("userPosts")]
        public IActionResult UserPost()
        {
            var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(_bearer_token);
            var id = jwtSecurityToken.Claims.FirstOrDefault(x => x.Type == "nameid")?.Value;

            var result = _postService.GetPostsByUser(Guid.Parse(id));
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
    }
}