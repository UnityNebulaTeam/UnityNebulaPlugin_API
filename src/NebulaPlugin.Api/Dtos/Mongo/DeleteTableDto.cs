

namespace NebulaPlugin.Api.Dtos.Mongo;

public class DeleteTableDto
{
    public string DbName { get; set; }= null!;
    public string Name { get; set; }= null!;
}
