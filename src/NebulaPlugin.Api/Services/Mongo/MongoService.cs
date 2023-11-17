using NebulaPlugin.Api.Dtos.Mongo;

using NebulaPlugin.Application.Mongo;


namespace NebulaPlugin.Api.Services.Mongo;

public class MongoService : IMongoService
{
    private readonly MongoManagement _manager;
    public MongoService()
    {
        _manager = new("mongodb+srv://Victory:7Aynras2-@gameserver.ptrocqn.mongodb.net/?retryWrites=true&w=majority");
    }

    // DELETE
    public async Task DeleteDatabaseAsync(DeleteMongoDbDatabaseDto database)
    {
        await _manager.DeleteDatabase(database.Name);
    }
    public async Task DeleteItemAsync(DeleteMongoDbItemDto item)
    {
        await _manager.DeleteItem(item.DbName, item.Name, item.Id);

    }
    public async Task DeleteTableAsync(DeleteMongoDbTableDto table)
    {
        await _manager.DeleteTable(table.DbName, table.Name);
    }

    // READ
    public async Task<List<MongoDbDatabaseDto>> GetAllDatabasesAsync()
    {
        List<MongoDbDatabaseDto> result = new();
        var databases = await _manager.ReadDatabases();
        foreach (var item in databases)
        {
            var dbItem = new MongoDbDatabaseDto{Name = item};
            result.Add(dbItem);
        }

        return result;
    }

    //CREATE 
    public async Task CreateDatabaseAsync(CreateMongoDbDatabaseDto database)
    {
        await _manager.CreateDatabase(database.Name);
    }

}
