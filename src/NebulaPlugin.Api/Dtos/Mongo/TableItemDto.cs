using MongoDB.Bson;

namespace NebulaPlugin.Api.Dtos.Mongo;

public record TableItemDto(BsonDocument? Doc);
