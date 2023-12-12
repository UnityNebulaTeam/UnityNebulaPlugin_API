using System.Text.Json;
using NebulaPlugin.Api.Dtos.Mongo;
using NebulaPlugin.Api.Dtos.Mongo.Responses;

namespace NebulaPlugin.Api.Services.Mongo;

public interface IMongoService
{
    //CREATE
    Task CreateDatabaseAsync(CreateDatabaseDto database);
    Task CreateTableAsync(CreateTableDto table);
    Task CreateItemAsync(CreateTableItemDto item);

    //READ
    Task<List<ReadDatabaseDto>> GetAllDatabasesAsync();
    Task<List<TableDto>> GetAllTablesAsync(ReadTableDto table);
    Task<List<JsonElement>> GetAllTableItemsAsync(ReadTableItemsDto item);

    //DELETE
    Task DeleteDatabaseAsync(DeleteDatabaseDto database);
    Task DeleteTableAsync(DeleteTableDto table);
    Task DeleteItemAsync(DeleteItemDto item);

    //UPDATE
    Task UpdateDatabase(UpdateDatabaseDto database);
    Task UpdateTable(UpdateTableDto table);
    Task UpdateTableItem(UpdateTableItemDto item);

}
