using MongoDB.Bson;

namespace NebulaPlugin.Api.Dtos.Mongo;

public record UpdateTableItemDto(string DbName, string TableName, BsonDocument Doc);