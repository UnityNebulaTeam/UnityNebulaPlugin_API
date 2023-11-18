using Microsoft.AspNetCore.Mvc;
using NebulaPlugin.Api.Dtos.Mongo;
using NebulaPlugin.Api.Services.Mongo;

namespace NebulaPlugin.Api.Controllers;

public class MongoController : BaseController
{
    private readonly IMongoService _service;

    public MongoController(IMongoService service) => _service = service;

    #region CREATE Methods
    [HttpPost("db")]
    public async Task<ActionResult> CreateDatabase(CreateMongoDbDatabaseDto database)
    {
        await _service.CreateDatabaseAsync(database);
        return Ok(Results.Created());
    }
    #endregion

    #region GET Methods

    // http://localhost:3131/api/mongo/db    
    [HttpGet("db")]
    public async Task<ActionResult<List<MongoDbDatabaseDto>>> GetDatabases()
    {
        var result = await _service.GetAllDatabasesAsync();

        return OkOrNotFound(result);
    }


    #endregion

    #region DELETE Methods

    // http://localhost:3131/api/mongo/db    
    [HttpDelete("db")]
    public async Task<ActionResult> DeleteDatabase([FromQuery] DeleteMongoDbDatabaseDto database)
    {

        await _service.DeleteDatabaseAsync(database);
        return Ok(Results.NoContent());

    }

    // http://localhost:3131/api/mongo/item 
    [HttpDelete("item")]
    public async Task<ActionResult> DeleteItem([FromQuery] DeleteMongoDbItemDto item)
    {
        await _service.DeleteItemAsync(item);
        return Ok(Results.NoContent());

    }

    // http://localhost:3131/api/mongo/table
    [HttpDelete("table")]
    public async Task<ActionResult> DeleteTable([FromQuery] DeleteMongoDbTableDto table)
    {
        await _service.DeleteTableAsync(table);
        return Ok(Results.NoContent());

    }

    #endregion

}
