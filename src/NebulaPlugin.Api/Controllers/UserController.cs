using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using NebulaPlugin.Api.Dtos.Mongo;
using NebulaPlugin.Api.Dtos.User;
using NebulaPlugin.Api.Services.User;

namespace NebulaPlugin.Api.Controllers;

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
        var res = await _userService.AddDatabaseAsync(databaseDto, UserId);

        return Ok(res);
    }
}
