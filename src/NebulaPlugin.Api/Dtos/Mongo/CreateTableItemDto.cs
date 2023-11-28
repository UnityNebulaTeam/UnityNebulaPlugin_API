using MongoDB.Bson;

namespace NebulaPlugin.Api.Dtos.Mongo;

public record CreateTableItemDto(string DbName, string TableName, BsonDocument Doc);

