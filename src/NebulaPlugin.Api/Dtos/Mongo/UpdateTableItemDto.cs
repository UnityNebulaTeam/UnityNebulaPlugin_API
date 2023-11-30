using System.Text.Json;

namespace NebulaPlugin.Api.Dtos.Mongo;

public class UpdateTableItemDto
{
    public string DbName { get; set; }
    public string TableName { get; set; }
    public JsonElement Doc { get; set; }
}