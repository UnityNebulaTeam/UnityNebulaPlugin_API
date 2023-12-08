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
        var res = await _manager.Mongo.CreateDatabaseAsync(database);
        return Ok(res);
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

        var res = await _manager.Mongo.DeleteDatabaseAsync(database);
        return Ok(res);

    }

    [HttpPut("db")]
    public async Task<ActionResult> UpdateDatabase([FromBody] UpdateDatabaseDto database)
    {
        var res = await _manager.Mongo.UpdateDatabase(database);
        return Ok(res);

    }

    #endregion

    #region TABLE Enpoints

    [HttpPost("table")]
    public async Task<ActionResult> CreateTableAsync([FromBody] CreateTableDto table)
    {
        var res = await _manager.Mongo.CreateTableAsync(table);
        return Created("", res);

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
        var res = await _manager.Mongo.DeleteTableAsync(table);
        return Ok(res);

    }

    [HttpPut("table")]
    public async Task<ActionResult> UpdateTable([FromBody] UpdateTableDto table)
    {
        var res = await _manager.Mongo.UpdateTable(table);
        return Ok(res);
    }

    #endregion

    #region TABLE ITEM Endpoints

    [HttpDelete("item")]
    public async Task<ActionResult> DeleteTableItem([FromQuery] DeleteItemDto item)
    {
        var res = await _manager.Mongo.DeleteItemAsync(item);
        return Ok(res);

    }

    [HttpPost("item")]
    public async Task<ActionResult> CreateTableItemAsync([FromBody] CreateTableItemDto item)
    {
        await _manager.Mongo.CreateItemAsync(item);
        return Created();

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
        var res = await _manager.Mongo.UpdateTableItem(item);

        Console.WriteLine(res);
        return Ok(res);
    }

    #endregion
}
