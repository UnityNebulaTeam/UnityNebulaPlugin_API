using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NebulaPlugin.Api.Models;

public class Database
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string ConnectionString { get; set; } = null!;

    [ForeignKey("User")]
    public string UserId { get; set; }
    public User User { get; set; }
}
