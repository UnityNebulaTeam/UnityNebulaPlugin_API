namespace NebulaPlugin.Api.Dtos.Mongo;

public class UpdateDatabaseDto
{
    public string Name { get; set; } = null!;
    public string NewDbName { get; set; } = null!;
}
