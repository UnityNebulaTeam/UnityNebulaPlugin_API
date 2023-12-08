using Microsoft.AspNetCore.Mvc;
using NebulaPlugin.Api.Dtos.Mongo;
using NebulaPlugin.Api.Services;

namespace NebulaPlugin.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    private readonly IServiceManager _manager;

    public TestController(IServiceManager manager)
    {
        _manager = manager;
    }

    [HttpGet("db")]
    public async Task<ActionResult<List<ReadDatabaseDto>>> GetDatabases()
    {
        var result = await _manager.Mongo.GetAllDatabasesAsync();

        return Ok(result);
    }

}
