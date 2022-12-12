using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using SocialNetwork.Business.Abstract;

namespace SocialNetwork.Api.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IUserRoleService _userRoleService;
        private readonly IUserService _userService;
        private readonly IPostService _postService;
        public AdminController(IUserRoleService userRoleService, IUserService userService, IPostService postService)
        {
            _userRoleService = userRoleService;
            _userService = userService;
            _postService = postService;
        }
        [HttpPost("addRoleToUser/{role}/{userId}")]
        public IActionResult AddRoleToUser(string role, Guid userId)
        {
            var result = _userRoleService.AddRole(role, userId);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("allUsers")]
        public IActionResult GetAllUsers()
        {
            var result = _userService.GetAllUsers();
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("allPosts")]
        public IActionResult GetAllPosts()
        {
            var result = _postService.GetAllPosts();
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }


    }
}