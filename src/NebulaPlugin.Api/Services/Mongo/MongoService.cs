using System.Text.Json;
using MongoDB.Bson;
using NebulaPlugin.Api.Dtos.Mongo;
using NebulaPlugin.Api.Dtos.Mongo.Responses;
using NebulaPlugin.Api.Helpers;
using NebulaPlugin.Application.Mongo;
using ZstdSharp.Unsafe;


namespace NebulaPlugin.Api.Services.Mongo;
public class MongoService : IMongoService
{
    private readonly MongoManagement _manager;
    public MongoService()
    {
        _manager = new("mongodb+srv://Victory:7Aynras2-@gameserver.ptrocqn.mongodb.net/?retryWrites=true&w=majority");
    }

    #region DELETE
    public async Task<DatabaseResponse> DeleteDatabaseAsync(DeleteDatabaseDto database)
    {
        var deleted = await _manager.DeleteDatabase(database.Name);

        if (!deleted)
            throw new Exception($"database deletion failed:{database.Name}");

        return new(database.Name);
    }
    public async Task<TableItemResponse> DeleteItemAsync(DeleteItemDto item)
    {
        var deleted = await _manager.DeleteItem(item.DbName, item.Name, item.Id);
        if (!deleted)
            throw new Exception("Item deletion failed");

        return new(item.Id);

    }
    public async Task<TableResponse> DeleteTableAsync(DeleteTableDto table)
    {
        var deleted = await _manager.DeleteTable(table.DbName, table.Name);
        if (!deleted)
            throw new Exception($"Delete table '{table.Name}' in database '{table.DbName}' failed.");

        return new(table.Name);
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
    public async Task<List<JsonElement>> GetAllTableItemsAsync(ReadTableItemsDto item)
    {
        List<JsonElement> tableItems = new();
        List<BsonDocument>? bsonItems = await _manager.ReadItems(item.DbName, item.TableName);

        bsonItems.ForEach(d => tableItems.Add(Helper.ConvertBsonDocumentToJsonElement(d)));

        return tableItems;
    }

    #endregion

    #region INSERT
    public async Task<DatabaseResponse> CreateDatabaseAsync(CreateDatabaseDto database)
    {
        var created = await _manager.CreateDatabase(database.Name, database.TableName);

        if (!created)
            throw new Exception($"'{database.Name}' database creation failed");

        return new(database.Name);
    }
    public async Task<TableResponse> CreateTableAsync(CreateTableDto table)
    {
        var created = await _manager.CreateTable(table.DbName, table.Name);

        if (!created)
            throw new Exception($"Creating table '{table.Name}' in database '{table.DbName}' failed.");

        return new(table.Name);
    }
    public async Task CreateItemAsync(CreateTableItemDto item)
    {
        BsonDocument bsonDoc = BsonDocument.Parse(item.Doc.ToString());
        var created = await _manager.CreateItem(item.DbName, item.TableName, bsonDoc);

        if (!created)
            throw new Exception("Item creation failed.");
    }

    #endregion

    #region UPDATE
    public async Task<DatabaseResponse> UpdateDatabase(UpdateDatabaseDto database)
    {
        var updated = await _manager.UpdateDatabase(database.Name, database.NewDbName);

        if (!updated)
            throw new Exception($"'{database.Name}' database update failed");

        return new(database.NewDbName);
    }
    public async Task<TableResponse> UpdateTable(UpdateTableDto table)
    {
        var updated = await _manager.UpdateTable(table.DbName, table.Name, table.NewTableName);

        if (!updated)
            throw new Exception($"Update table '{table.Name}' in database '{table.DbName}' failed.");

        return new(table.NewTableName);
    }
    public async Task<TableItemResponse> UpdateTableItem(UpdateTableItemDto item)
    {
        BsonDocument bsonDoc = BsonDocument.Parse(item.Doc.ToString());
        bsonDoc["_id"] = new ObjectId(bsonDoc["_id"].AsString);

        var updated = await _manager.UpdateItem(item.DbName, item.TableName, bsonDoc);

        if (!updated)
            throw new Exception("Item creation failed.");

        return new(bsonDoc["_id"].ToString());

    }

    #endregion

}
