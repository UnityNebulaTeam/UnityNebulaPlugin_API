using NebulaPlugin.Api.Dtos.Mongo;

namespace NebulaPlugin.Api.Services.Mongo;

public class MongoService : IMongoService
{
    public void DeleteDatabase(DeleteMongoDbDatabaseDto database)
    {
        throw new NotImplementedException();
    }

    public void DeleteItem(DeleteMongoDbItemDto item)
    {
        throw new NotImplementedException();
    }

    public void DeleteTable(DeleteMongoDbTableDto table)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<MongoDbDatabaseDto> GetAllDatabases()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<ReadMongoDbTableDto> GetAllTables()
    {
        throw new NotImplementedException();
    }
}
