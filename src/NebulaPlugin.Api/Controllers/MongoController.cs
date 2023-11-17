using Microsoft.AspNetCore.Mvc;
using NebulaPlugin.Api.Services.Mongo;

namespace NebulaPlugin.Api.Controllers;

public class MongoController : BaseController
{
    IMongoService mongoService;
    public MongoController(IMongoService _service)=>mongoService=_service;

    [HttpGet]
    public async Task Test2()
    {
      mongoService.TestGetDatabases();
    }
}
