namespace NebulaPlugin.Api.Dtos.Mongo;

public class CreateTableDto
{
    public string DbName { get; set; } = null!;
    public string Name { get; set; } = null!;
}
