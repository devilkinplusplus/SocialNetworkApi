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
    [ApiController]
    [Route("api/[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [Authorize(Roles = "User")]
        [HttpPost("sendMessage")]
        public IActionResult SendMessage(string message)
        {
            var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(_bearer_token);
            var id = jwtSecurityToken.Claims.FirstOrDefault(x => x.Type == "nameid")?.Value;

            var result = _contactService.SendMessage(Guid.Parse(id), message);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("getMessages")]
        public IActionResult GetAllMessages()
        {
            var result = _contactService.GetMessages();
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return NotFound(result.Message);
        }
    }
}