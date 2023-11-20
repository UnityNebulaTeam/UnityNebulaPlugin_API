using MongoDB.Bson;

namespace NebulaPlugin.Api.Services.Mongo;

public interface IMongoService
{
    Task<List<BsonDocument>> TestGetDatabases();
}
