using MongoDB.Bson;

namespace NebulaPlugin.Api.Dtos.Mongo;

public class CreateTableItemDto
{
    public string DbName { get; set; } = null!;
    public string TableName { get; set; } = null!;
    public BsonDocument doc { get; set; } = null!;

}
