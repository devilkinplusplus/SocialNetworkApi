using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using SocialNetwork.Business.Abstract;
using static SocialNetwork.Entities.DTOs.UserDTO;

namespace SocialNetwork.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/v1/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IWebHostEnvironment _environment;

        public UserController(IUserService userService, IWebHostEnvironment environment)
        {
            _userService = userService;
            _environment = environment;
        }

        [HttpGet("getuser/{email}")]
        public IActionResult GetUserByEmail(string email)
        {
            return Ok(_userService.GetUserByEmail(email));
        }

        [HttpGet("getUserPosts")]
        public IActionResult GetUserPosts()
        {
            var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(_bearer_token);
            var id = jwtSecurityToken.Claims.FirstOrDefault(x => x.Type == "nameid")?.Value;
            var result = _userService.GetUserPosts(Guid.Parse(id));

            if (result.Success)
                return Ok(result.Data);
            return BadRequest(result.Message);
        }

        [HttpPut("updateUser")]
        public IActionResult UpdateUser([FromForm] UpdateUserDTO model)
        {
            var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(_bearer_token);
            var id = jwtSecurityToken.Claims.FirstOrDefault(x => x.Type == "nameid")?.Value;

            if (model.PhotoUrl != null)
            {
                if (!Directory.Exists(_environment.WebRootPath + "\\images\\profilePictures\\"))
                {
                    Directory.CreateDirectory(_environment.WebRootPath + "\\images\\profilePictures\\");
                }
                using (FileStream fileStream = System.IO.File.Create(_environment.WebRootPath + "\\images\\profilePictures\\" + model.PhotoUrl.FileName))
                {
                    model.PhotoUrl.CopyTo(fileStream);
                    fileStream.Flush();
                }
            }


            var result = _userService.UpdateUser(model, Guid.Parse(id));
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
    }
}