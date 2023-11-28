
namespace NebulaPlugin.Api.Dtos.Mongo;

public class DeleteItemDto
{
    public string DbName { get; set; } = null!;
    public string Name { get; set; }= null!;
    public string Id { get; set; }= null!;
}
