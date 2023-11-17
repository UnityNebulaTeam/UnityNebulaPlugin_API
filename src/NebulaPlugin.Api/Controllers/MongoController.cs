using Microsoft.AspNetCore.Mvc;

namespace NebulaPlugin.Api.Controllers;

public class MongoController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<string>> Test()
    {
        return OkOrNotFound("world");
    }
}
