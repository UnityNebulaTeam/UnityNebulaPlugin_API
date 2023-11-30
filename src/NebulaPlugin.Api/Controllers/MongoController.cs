using Microsoft.AspNetCore.Mvc;
using NebulaPlugin.Api.Dtos.Mongo;
using NebulaPlugin.Api.Services.Mongo;

namespace NebulaPlugin.Api.Controllers;

public class MongoController : BaseController
{
    private readonly IMongoService _service;

    public MongoController(IMongoService service) => _service = service;

    #region DB Enpoints


    [HttpPost("db")]
    public async Task<ActionResult> CreateDatabase([FromForm] CreateDatabaseDto database)
    {
        await _service.CreateDatabaseAsync(database);
        return Ok(Results.Created());
    }

    // http://localhost:3131/api/mongo/db    
    [HttpGet("db")]
    public async Task<ActionResult<List<ReadDatabaseDto>>> GetDatabases()
    {
        var result = await _service.GetAllDatabasesAsync();

        return OkOrNotFound(result);
    }

    // http://localhost:3131/api/mongo/db    
    [HttpDelete("db")]
    public async Task<ActionResult> DeleteDatabase([FromQuery] DeleteDatabaseDto database)
    {

        await _service.DeleteDatabaseAsync(database);
        return Ok(Results.NoContent());

    }
    [HttpPut("db")]
    public async Task<ActionResult> UpdateDatabase([FromForm] UpdateDatabaseDto database)
    {
        await _service.UpdateDatabase(database);
        return Ok(Results.NoContent());

    }

    #endregion

    #region TABLE Enpoints

    [HttpPost("table")]
    public async Task<ActionResult> CreateTableAsync([FromForm] CreateTableDto table)
    {
        await _service.CreateTableAsync(table);
        return Ok(Results.Created());

    }

    [HttpGet("table")]
    public async Task<ActionResult<List<TableDto>>> GetTables([FromQuery] ReadTableDto table)
    {
        var result = await _service.GetAllTablesAsync(table);
        return OkOrNotFound(result);
    }

    [HttpDelete("table")]
    public async Task<ActionResult> DeleteTable([FromQuery] DeleteTableDto table)
    {
        await _service.DeleteTableAsync(table);
        return Ok(Results.NoContent());

    }

    [HttpPut("table")]
    public async Task<ActionResult> UpdateTable([FromForm] UpdateTableDto table)
    {
        await _service.UpdateTable(table);
        return Ok(Results.NoContent());
    }

    #endregion

    #region TABLE ITEM Endpoints

    [HttpDelete("item")]
    public async Task<ActionResult> DeleteTableItem([FromQuery] DeleteItemDto item)
    {
        await _service.DeleteItemAsync(item);
        return Ok(Results.NoContent());

    }

    [HttpPost("item")]
    public async Task<ActionResult> CreateTableItemAsync([FromForm] CreateTableItemDto item)
    {
        await _service.CreateItemAsync(item);
        return Ok(Results.Created());

    }

    // !
    [HttpGet("item")]
    public async Task<ActionResult<List<TableItemDto>>> GetTableItems([FromQuery] ReadTableItemsDto item)
    {
        var result = await _service.GetAllTableItemsAsync(item);
        var jsonObj = Helpers.Helper.ConvertBsonDocumentToJson(result);
        return Ok(jsonObj);

    }

    [HttpPut("item")]
    public async Task<ActionResult> UpdateTableItem([FromBody] UpdateTableItemDto item)
    {
        await _service.UpdateTableItem(item);
        return Ok(Results.Created());
    }

    #endregion
}
