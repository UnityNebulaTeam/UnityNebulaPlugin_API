using System.Text.Json;
using MongoDB.Bson;

namespace NebulaPlugin.Api.Dtos.Mongo;

public record CreateTableItemDto
{
    public string DbName { get; init; }
    public string TableName { get; init; }
    public JsonElement Doc { get; init; }
}

