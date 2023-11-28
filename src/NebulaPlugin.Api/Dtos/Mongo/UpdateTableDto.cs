namespace NebulaPlugin.Api.Dtos.Mongo;

public class UpdateTableDto
{
    public string DbName { get; set; }  = null!;
    public string Name { get; set; }  = null!;
    public string NewTableName { get; set; }  = null!;
}
