using MongoDB.Bson;

namespace NebulaPlugin.Api.Dtos.Mongo;

public class UpdateTableItemDto
{
    public string DbName { get; set; } = null!;
    public string TableName { get; set; } = null!;
    public BsonDocument Doc { get; set; } = null!;
}
