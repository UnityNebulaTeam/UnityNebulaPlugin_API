using Microsoft.AspNetCore.Mvc;
using NebulaPlugin.Api.Dtos.User;
using NebulaPlugin.Api.Services.User;

namespace NebulaPlugin.Api.Controllers;


public class AuthController : BaseController
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] CreateUserDto userDto)
    {
        var res = await _authService.CreateUserAsync(userDto);

        return StatusCode(201);
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] AuthenticateUserDto userDto)
    {
        var res = await _authService.ValidateUserAsync(userDto);

        return Ok(res);
    }

    [HttpPost("refresh")]
    public async Task<ActionResult> RefreshToken([FromBody] TokenDto tokenDto)
    {
        var res = await _authService.RefreshTokenAsync(tokenDto);

        return Ok(res);
    }
}
