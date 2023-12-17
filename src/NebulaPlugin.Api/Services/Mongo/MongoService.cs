using System.Text.Json;
using MongoDB.Bson;
using NebulaPlugin.Api.Dtos.Mongo;
using NebulaPlugin.Api.Dtos.Mongo.Responses;
using NebulaPlugin.Api.EFCore;
using NebulaPlugin.Api.Helpers;
using NebulaPlugin.Application.Mongo;
using NebulaPlugin.Common.Enums;
using NebulaPlugin.Common.Exceptions.MongoExceptions;


namespace NebulaPlugin.Api.Services.Mongo;

public class MongoService : IMongoService
{
    private readonly MongoManagement _manager;

    public MongoService(IHttpContextAccessor httpContextAccessor, AppDbContext dbContext)
    {

        string? _connectionString = Helper.GetConnectionStringFromHttpContext(httpContextAccessor.HttpContext, dbContext, DbTypes.MONGODB);
        _manager = new(_connectionString);
    }

    #region DELETE
    public async Task DeleteDatabaseAsync(DeleteDatabaseDto database)
    {
        var deleted = await _manager.DeleteDatabase(database.Name);

        if (!deleted)
            throw new MongoOperationFailedException(database.Name, "Delete failed");

        ;
    }
    public async Task DeleteItemAsync(DeleteItemDto item)
    {
        var deleted = await _manager.DeleteItem(item.DbName, item.Name, item.Id);
        if (!deleted)
            throw new MongoOperationFailedException(item.Id, "id delete failed");



    }
    public async Task DeleteTableAsync(DeleteTableDto table)
    {
        var deleted = await _manager.DeleteTable(table.DbName, table.Name);
        if (!deleted)
            throw new MongoOperationFailedException(table.Name, "table delete failed");


    }

    #endregion

    #region READ
    public async Task<List<ReadDatabaseDto>> GetAllDatabasesAsync()
    {
        List<ReadDatabaseDto> result = new();
        var databases = await _manager.ReadDatabases();

        if (databases is not null)
            databases.ForEach(db => result.Add(new ReadDatabaseDto(db)));

        return result;
    }
    public async Task<List<TableDto>> GetAllTablesAsync(ReadTableDto table)
    {
        List<TableDto> tables = new();
        var dbTables = await _manager.ReadTables(table.DbName);

        if (dbTables is not null)
            dbTables.ForEach(t => tables.Add(new TableDto(t)));
        return tables;
    }
    public async Task<List<JsonElement>> GetAllTableItemsAsync(ReadTableItemsDto item)
    {
        List<JsonElement> tableItems = new();
        List<BsonDocument>? bsonItems = await _manager.ReadItems(item.DbName, item.TableName);

        if (bsonItems is not null)
            bsonItems.ForEach(d => tableItems.Add(Helper.ConvertBsonDocumentToJsonElement(d)));

        return tableItems;
    }

    #endregion

    #region INSERT
    public async Task CreateDatabaseAsync(CreateDatabaseDto database)
    {
        var created = await _manager.CreateDatabase(database.Name, database.TableName);

        if (!created)
            throw new MongoOperationFailedException(database.Name, "create failed");


    }
    public async Task CreateTableAsync(CreateTableDto table)
    {
        var created = await _manager.CreateTable(table.DbName, table.Name);

        if (!created)
            throw new MongoOperationFailedException(table.Name, "table create failed");

    }
    public async Task CreateItemAsync(CreateTableItemDto item)
    {
        BsonDocument bsonDoc = BsonDocument.Parse(item.Doc.ToString());
        var created = await _manager.CreateItem(item.DbName, item.TableName, bsonDoc);

        if (!created)
            throw new MongoOperationFailedException(item.TableName, "table item create failed");
    }

    #endregion

    #region UPDATE
    public async Task UpdateDatabase(UpdateDatabaseDto database)
    {
        var updated = await _manager.UpdateDatabase(database.Name, database.NewDbName);

        if (!updated)
            throw new MongoOperationFailedException(database.Name, " update failed");

    }
    public async Task UpdateTable(UpdateTableDto table)
    {
        var updated = await _manager.UpdateTable(table.DbName, table.Name, table.NewTableName);

        if (!updated)
            throw new MongoOperationFailedException(table.NewTableName, "table update failed");

    }
    public async Task UpdateTableItem(UpdateTableItemDto item)
    {
        BsonDocument bsonDoc = BsonDocument.Parse(item.Doc.ToString());

        if (!bsonDoc.Contains("_id"))
            throw new MongoEmptyValueException("Item '_id' value cannot be null or whitespace");


        bsonDoc["_id"] = new ObjectId(bsonDoc["_id"].AsString);

        var updated = await _manager.UpdateItem(item.DbName, item.TableName, bsonDoc);

        if (!updated)
            throw new MongoOperationFailedException(item.TableName, "table item update failed");

    }

    #endregion

}
