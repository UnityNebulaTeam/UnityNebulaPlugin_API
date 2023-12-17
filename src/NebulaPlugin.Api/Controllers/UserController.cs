using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NebulaPlugin.Api.Dtos.User;
using NebulaPlugin.Api.Services.User;

namespace NebulaPlugin.Api.Controllers;

[Authorize]
public class UserController : BaseController
{
    private string UserId => new(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("db")]
    public async Task<IActionResult> GetConnections()
    {
        var res = await _userService.GetUsersConnections(UserId);
        return Ok(res);
    }

    [HttpPost("db")]
    public async Task<IActionResult> CreateConnection(AddUserDatabaseDto databaseDto)
    {
        await _userService.AddConnectionAsync(databaseDto, UserId);

        return StatusCode(201);
    }

    [HttpPut("db/{type}")]
    public async Task<IActionResult> UpdateConnection(UpdateUserDatabaseDto databaseDto, [FromRoute] string type)
    {
        await _userService.UpdateConnectionAsync(databaseDto, UserId, type);

        return StatusCode(204);
    }

    [HttpGet("me")]
    public async Task<IActionResult> GetUserData()
    {
        var res = await _userService.GetUserDataAsync(UserId);

        return Ok(res);
    }
}
