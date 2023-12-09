using Microsoft.AspNetCore.Mvc;
using NebulaPlugin.Api.Attributes;
using NebulaPlugin.Api.Dtos.Mongo;
using NebulaPlugin.Api.Services;

namespace NebulaPlugin.Api.Controllers;

[AuthorizeDbType("MONGO")]
public class MongoController : BaseController
{
    // private readonly IMongoService _service;

    private readonly IServiceManager _manager;

    public MongoController(IServiceManager manager) => _manager = manager;

    #region DB Enpoints


    [HttpPost("db")]
    public async Task<ActionResult> CreateDatabase([FromBody] CreateDatabaseDto database)
    {
        await _manager.Mongo.CreateDatabaseAsync(database);
        return StatusCode(201);
    }

    [HttpGet("db")]
    public async Task<ActionResult<List<ReadDatabaseDto>>> GetDatabases()
    {
        var result = await _manager.Mongo.GetAllDatabasesAsync();

        return OkOrNotFound(result);
    }

    [HttpDelete("db")]
    public async Task<ActionResult> DeleteDatabase([FromQuery] DeleteDatabaseDto database)
    {

        await _manager.Mongo.DeleteDatabaseAsync(database);
        return StatusCode(204);

    }

    [HttpPut("db")]
    public async Task<ActionResult> UpdateDatabase([FromBody] UpdateDatabaseDto database)
    {
        await _manager.Mongo.UpdateDatabase(database);
        return StatusCode(204);

    }

    #endregion

    #region TABLE Enpoints

    [HttpPost("table")]
    public async Task<ActionResult> CreateTableAsync([FromBody] CreateTableDto table)
    {
        await _manager.Mongo.CreateTableAsync(table);
        return StatusCode(201);

    }

    [HttpGet("table")]
    public async Task<ActionResult<List<TableDto>>> GetTables([FromQuery] ReadTableDto table)
    {
        var result = await _manager.Mongo.GetAllTablesAsync(table);
        return OkOrNotFound(result);
    }

    [HttpDelete("table")]
    public async Task<ActionResult> DeleteTable([FromQuery] DeleteTableDto table)
    {
        await _manager.Mongo.DeleteTableAsync(table);
        return StatusCode(204);

    }

    [HttpPut("table")]
    public async Task<ActionResult> UpdateTable([FromBody] UpdateTableDto table)
    {
        await _manager.Mongo.UpdateTable(table);
        return StatusCode(204);

    }

    #endregion

    #region TABLE ITEM Endpoints

    [HttpDelete("item")]
    public async Task<ActionResult> DeleteTableItem([FromQuery] DeleteItemDto item)
    {
        await _manager.Mongo.DeleteItemAsync(item);
        return StatusCode(204);

    }

    [HttpPost("item")]
    public async Task<ActionResult> CreateTableItemAsync([FromBody] CreateTableItemDto item)
    {
        await _manager.Mongo.CreateItemAsync(item);
        return StatusCode(201);

    }

    // !
    [HttpGet("item")]
    public async Task<ActionResult<List<TableItemDto>>> GetTableItems([FromQuery] ReadTableItemsDto item)
    {
        var result = await _manager.Mongo.GetAllTableItemsAsync(item);
        // var jsonObj = Helpers.Helper.ConvertBsonDocumentToJson(result);
        return Ok(result);

    }

    [HttpPut("item")]
    public async Task<ActionResult> UpdateTableItem([FromBody] UpdateTableItemDto item)
    {
        await _manager.Mongo.UpdateTableItem(item);

        return StatusCode(204);
    }

    #endregion
}
