using MongoDB.Bson;
using MongoDB.Bson.Serialization;
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

    #region DELETE
    public async Task DeleteDatabaseAsync(DeleteDatabaseDto database)
    {
        var created = await _manager.DeleteDatabase(database.Name);

        if (!created)
            throw new Exception($"database deletion failed:{database.Name}");

        // return new DeleteDatabaseDto(database.Name);
    }

    public async Task DeleteItemAsync(DeleteItemDto item)
    {
        await _manager.DeleteItem(item.DbName, item.Name, item.Id);

    }
    public async Task DeleteTableAsync(DeleteTableDto table)
    {
        await _manager.DeleteTable(table.DbName, table.Name);
    }

    #endregion
    #region READ
    public async Task<List<ReadDatabaseDto>> GetAllDatabasesAsync()
    {
        List<ReadDatabaseDto> result = new();
        var databases = await _manager.ReadDatabases();
        foreach (var item in databases)
        {
            var dbItem = new ReadDatabaseDto(item);
            result.Add(dbItem);
        }

        return result;
    }
    public async Task<List<TableDto>> GetAllTablesAsync(ReadTableDto table)
    {
        List<TableDto> tables = new();
        var dbTables = await _manager.ReadTables(table.DbName);
        dbTables.ForEach(t => tables.Add(new TableDto(t)));
        return tables;
    }
    public async Task<List<TableItemDto>> GetAllTableItemsAsync(ReadTableItemsDto item)
    {
        List<TableItemDto> tableItems = new();
        var dbTableItems = await _manager.ReadItems(item.DbName, item.TableName);
        dbTableItems.ForEach(i => tableItems.Add(new TableItemDto(i)));

        return tableItems;
    }

    #endregion
    #region INSERT
    public async Task CreateDatabaseAsync(CreateDatabaseDto database)
    {
        await _manager.CreateDatabase(database.Name);
    }
    public async Task CreateTableAsync(CreateTableDto table)
    {
        await _manager.CreateTable(table.DbName, table.Name);
    }
    public async Task CreateItemAsync(CreateTableItemDto item)
    {
        await _manager.CreateItem(item.DbName, item.TableName, item.Doc);
    }

    #endregion
    #region UPDATE
    public async Task UpdateDatabase(UpdateDatabaseDto database)
    {
        await _manager.UpdateDatabase(database.Name, database.NewDbName);
    }
    public async Task UpdateTable(UpdateTableDto table)
    {
        await _manager.UpdateTable(table.DbName, table.Name, table.NewTableName);
    }
    public async Task UpdateTableItem(UpdateTableItemDto item)
    {
        Console.WriteLine(item.Doc);
        BsonDocument bsonDoc = BsonDocument.Parse(item.Doc.ToString());
        bsonDoc["_id"]=new ObjectId(bsonDoc["_id"].AsString);
        Console.WriteLine(bsonDoc);
        await _manager.UpdateItem(item.DbName, item.TableName, bsonDoc);
    }

    #endregion

}
