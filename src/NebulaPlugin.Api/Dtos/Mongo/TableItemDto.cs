using MongoDB.Bson;

namespace NebulaPlugin.Api.Dtos.Mongo;

public class TableItemDto
{
    public BsonDocument? Doc { get; set; }
}
