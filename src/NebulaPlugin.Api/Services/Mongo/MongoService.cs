using NebulaPlugin.Api.Dtos.Mongo;

namespace NebulaPlugin.Api.Services.Mongo;
public class MongoService : IMongoService
{
    public void DeleteDatabase(DeleteMongoDbDatabaseDto database)
    {
        Console.WriteLine(database);
    }

    public void DeleteItem(DeleteMongoDbItemDto item)
    {
        Console.WriteLine(item);

    }

    public void DeleteTable(DeleteMongoDbTableDto table)
    {
        Console.WriteLine(table);

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
