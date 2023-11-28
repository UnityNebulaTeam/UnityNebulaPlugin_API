using NebulaPlugin.Api.Dtos.Mongo;

namespace NebulaPlugin.Api.Services.Mongo;

public interface IMongoService
{
    //CREATE
    Task CreateDatabaseAsync(CreateDatabaseDto database);
    //READ
    Task<List<ReadDatabaseDto>> GetAllDatabasesAsync();    
    Task<List<string>> GetAllDatabaseTablesAsync(string dbName);

    //DELETE
    Task DeleteDatabaseAsync(DeleteDatabaseDto database);
    Task DeleteTableAsync(DeleteTableDto table);
    Task DeleteItemAsync(DeleteItemDto item);   

}
