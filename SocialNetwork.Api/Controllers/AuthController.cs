using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Business.Abstract;
using static SocialNetwork.Entities.DTOs.UserDTO;

namespace SocialNetwork.Api.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{

    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }


    [HttpPost("login")]
    public IActionResult Login(LoginDTO login)
    {
        var result = _authService.Login(login);
        if (result.Success)
        {
            return Ok(new {status = 200, message = result.Message });
        }
        return BadRequest(result.Message);
    }

     [HttpPost("register")]
    public IActionResult Register(RegisterDTO model)
    {
        var result = _authService.Register(model);
        if (result.Success)
        {
            return Ok(result.Message);
        }
        return BadRequest(result.Message);
    }
}
