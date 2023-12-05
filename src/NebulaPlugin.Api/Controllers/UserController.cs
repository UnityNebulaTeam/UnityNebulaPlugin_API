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
        var res = await _userService.AddDatabaseAsync(databaseDto, UserId);

        return Ok(res);
    }

    [HttpGet("me")]
    public async Task<IActionResult> GetUserData()
    {
        var res = await _userService.GetUserDataAsync(UserId);
        // return Ok(new
        // {
        //     Username = "test_3131",
        //     Email = "test@mail.com",
        //     Dbs = new List<string>() { "MONGO", "SQLITE" }
        // });

        return Ok(res);
    }
}
