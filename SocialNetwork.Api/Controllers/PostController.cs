using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using SocialNetwork.Business.Abstract;
using SocialNetwork.Entities.Concrete;
using static SocialNetwork.Entities.DTOs.PostDTO;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace SocialNetwork.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]/v1")]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly IUserService _userService;
        private readonly IWebHostEnvironment _environment;
        public PostController(IPostService postService, IUserService userService, IWebHostEnvironment environment)
        {
            _postService = postService;
            _userService = userService;
            _environment = environment;
        }


        [HttpPost("share")]
        public async Task<IActionResult> Share([FromForm] SharePostDTO model)
        {
            var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(_bearer_token);
            var id = jwtSecurityToken.Claims.FirstOrDefault(x => x.Type == "nameid")?.Value;

            if (model.photoUrl != null)
            {
                if (!Directory.Exists(_environment.WebRootPath + "\\images\\posts\\"))
                {
                    Directory.CreateDirectory(_environment.WebRootPath + "\\images\\posts\\");
                }
                using (FileStream fileStream = System.IO.File.Create(_environment.WebRootPath + "\\images\\posts\\" + model.photoUrl.FileName))
                {
                    model.photoUrl.CopyTo(fileStream);
                    fileStream.Flush();
                }
            }

            var result = _postService.Share(model, Guid.Parse(id));
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpPut("delete")]
        public IActionResult Delete(int postId)
        {
            var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(_bearer_token);
            var id = jwtSecurityToken.Claims.FirstOrDefault(x => x.Type == "nameid")?.Value;


            var result = _postService.Delete(postId, Guid.Parse(id));
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

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

        [HttpGet("suggestionPosts")]
        public IActionResult Suggestions()
        {
            var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(_bearer_token);
            var id = jwtSecurityToken.Claims.FirstOrDefault(x => x.Type == "nameid")?.Value;

            var result = _postService.Suggestions(Guid.Parse(id));
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
    }
}