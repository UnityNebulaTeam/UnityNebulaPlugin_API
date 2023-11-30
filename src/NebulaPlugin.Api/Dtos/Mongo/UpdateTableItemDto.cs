using System.Text.Json;
using MongoDB.Bson;

namespace NebulaPlugin.Api.Dtos.Mongo;

public record UpdateTableItemDto(string DbName, string TableName)
{
    public string DbName { get; init; }
    public string TableName { get; init; }
    public JsonElement Doc { get; init; }
}