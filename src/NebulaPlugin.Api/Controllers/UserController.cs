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
    public async Task<IActionResult> GetDbs()
    {
        var res = await _userService.GetUsersDatabases(UserId);
        return Ok(res);
    }

    [HttpPost("db")]
    public async Task<IActionResult> CreateDb(AddUserDatabaseDto databaseDto)
    {
        await _userService.AddDatabaseAsync(databaseDto, UserId);

        return StatusCode(201);
    }

    [HttpPut("db/{type}")]
    public async Task<IActionResult> UpdateDb(UpdateUserDatabaseDto databaseDto, [FromRoute] string type)
    {
        await _userService.UpdateDatabaseAsync(databaseDto, UserId, type);

        return StatusCode(204);
    }

    [HttpGet("me")]
    public async Task<IActionResult> GetUserData()
    {
        var res = await _userService.GetUserDataAsync(UserId);

        return Ok(res);
    }
}
