using Microsoft.AspNetCore.Mvc;
using NebulaPlugin.Api.Dtos.Mongo;
using NebulaPlugin.Api.Services.Mongo;

namespace NebulaPlugin.Api.Controllers;

public class MongoController : BaseController
{
    private readonly IMongoService _service;

    public MongoController(IMongoService service) => _service = service;
    
    #region GET Methods

    // http://localhost:3131/api/mongo/db    
    [HttpGet("/db")]
    public async Task<ActionResult<MongoDbDatabaseDto>> GetDatabases()
    {
        _service.GetAllDatabases();
        return Ok(200);
    }    


    #endregion

    #region DELETE Methods

    // http://localhost:3131/api/mongo/db    
    [HttpDelete("/db")]
    public async Task<ActionResult> DeleteDatabase([FromQuery] DeleteMongoDbDatabaseDto database)
    {
        _service.DeleteDatabase(database);
        return Ok(201);
    }

    // http://localhost:3131/api/mongo/item 
    [HttpDelete("/item")]
    public async Task<ActionResult> DeleteItem([FromQuery] DeleteMongoDbItemDto item)
    {
        _service.DeleteItem(item);
        return Ok(201);
    }

    // http://localhost:3131/api/mongo/table
    [HttpDelete("/table")]
    public async Task<ActionResult> DeleteTable([FromQuery] DeleteMongoDbTableDto table)
    {
        _service.DeleteTable(table);
        return Ok(201);
    }

    #endregion

}
