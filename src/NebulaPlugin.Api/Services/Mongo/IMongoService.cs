using NebulaPlugin.Api.Dtos.Mongo;

namespace NebulaPlugin.Api.Services.Mongo;

public interface IMongoService
{
    IEnumerable<MongoDbDatabaseDto> GetAllDatabases();
    
    IEnumerable<ReadMongoDbTableDto> GetAllTables();

    void DeleteDatabase(DeleteMongoDbDatabaseDto database);
    void DeleteTable(DeleteMongoDbTableDto table);
    void DeleteItem(DeleteMongoDbItemDto item);   

}
