using NebulaPlugin.Api.Dtos.Mongo;

namespace NebulaPlugin.Api.Services.Mongo;

public interface IMongoService
{
    //CREATE
    Task CreateDatabaseAsync(CreateMongoDbDatabaseDto database);

    //READ
    Task<List<MongoDbDatabaseDto>> GetAllDatabasesAsync();    

    Task DeleteDatabaseAsync(DeleteMongoDbDatabaseDto database);
    Task DeleteTableAsync(DeleteMongoDbTableDto table);
    Task DeleteItemAsync(DeleteMongoDbItemDto item);   

}
