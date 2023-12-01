using System.Text.Json;
using NebulaPlugin.Api.Dtos.Mongo;
using NebulaPlugin.Api.Dtos.Mongo.Responses;

namespace NebulaPlugin.Api.Services.Mongo;

public interface IMongoService
{
    //CREATE
    Task<DatabaseResponse> CreateDatabaseAsync(CreateDatabaseDto database);
    Task<TableResponse> CreateTableAsync(CreateTableDto table);
    Task CreateItemAsync(CreateTableItemDto item);

    //READ
    Task<List<ReadDatabaseDto>> GetAllDatabasesAsync();    
    Task<List<TableDto>> GetAllTablesAsync(ReadTableDto table);
    Task<List<JsonElement>> GetAllTableItemsAsync(ReadTableItemsDto item);

    //DELETE
    Task<DatabaseResponse> DeleteDatabaseAsync(DeleteDatabaseDto database);
    Task<TableResponse> DeleteTableAsync(DeleteTableDto table);
    Task<TableItemResponse> DeleteItemAsync(DeleteItemDto item); 

    //UPDATE
    Task<DatabaseResponse> UpdateDatabase(UpdateDatabaseDto database);
    Task<TableResponse> UpdateTable(UpdateTableDto table);
    Task<TableItemResponse> UpdateTableItem(UpdateTableItemDto item);

}
