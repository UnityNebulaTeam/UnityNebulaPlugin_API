using System.Text.Json;

namespace NebulaPlugin.Api.Dtos.Mongo;

public record UpdateTableItemDto
{
    public string DbName { get; init; }
    public string TableName { get; init; }
    public JsonElement Doc { get; init; }
}