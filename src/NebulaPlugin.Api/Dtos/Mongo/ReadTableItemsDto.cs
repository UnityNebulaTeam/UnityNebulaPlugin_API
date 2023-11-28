namespace NebulaPlugin.Api.Dtos.Mongo;

public class ReadTableItemsDto
{
    public string DbName { get; set; } = null!;
    public string TableName { get; set; } = null!;
}
