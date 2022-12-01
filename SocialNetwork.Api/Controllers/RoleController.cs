using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using SocialNetwork.Business.Abstract;
using SocialNetwork.Core.Entities.Concrete;
using static SocialNetwork.Entities.DTOs.RoleDTO;

namespace SocialNetwork.Api.Controllers
{
    // [Authorize(Roles = "Admin")]
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpPost("createRole")]
        public IActionResult CreateRole(CreateRoleDTO role)
        {
            var result = _roleService.Create(role);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("getAllRoles")]
        public IActionResult RoleList()
        {
            var result = _roleService.ListAllRoles();
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpPut("updateRole/{roleId}")]
        public IActionResult UpdateRole(CreateRoleDTO role, Guid roleId)
        {
            var result = _roleService.Update(role, roleId);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
    }
}