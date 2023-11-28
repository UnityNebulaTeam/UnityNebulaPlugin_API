namespace NebulaPlugin.Api.Dtos.Mongo;

public record UpdateTableDto(string DbName, string Name, string NewTableName);